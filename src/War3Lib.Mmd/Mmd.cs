using System.Collections.Generic;
using System.Linq;

using static War3Api.Common;

namespace War3Lib.Mmd
{
    // Based on code from https://gist.github.com/Promises/0b5c91a14312bb6da7c248edd4a3ab22
    // and https://wiki.wc3stats.com/images/7/75/W3mmd.txt
    public static class Mmd
    {
        internal const string KeyVal = "val:";
        internal const string KeyChk = "chk:";

        private const float ClockStartTime = 999999999f;
        private const float TickInterval = 0.37f;

        private const string Filename = "MMD.Dat";
        private const string EscapedCharacters = @" \";

        private const string EventsKey = "events";
        private const string TypesKey = "types";

        private const int LibraryVersion = 1;
        private const int MinimumParserVersion = 1;

        private static readonly EventQueue _eventQueue;
        private static readonly Queue<Node> _nodes;

        private static readonly gamecache _gamecache;
        private static readonly timer _clock;

        private static Senders _senders;
        private static int _messageCount;

        private static bool _initialized;
        private static bool _debugMessagesEnabled;

        public static event System.EventHandler Initialized;
        public static event System.EventHandler MessagesProcessed;

        static Mmd()
        {
            var t = CreateTrigger();
            TriggerRegisterTimerEvent(t, 0f, false);
            TriggerAddCondition(t, Condition(Init));

            _eventQueue = new EventQueue();
            _nodes = new Queue<Node>();

            FlushGameCache(InitGameCache(Filename));
            _gamecache = InitGameCache(Filename);

            _clock = CreateTimer();
            TimerStart(_clock, ClockStartTime, false, null);

            _senders = Senders.Naive;
            _messageCount = 0;
        }

        /// <summary>
        /// Gets the elapsed game time.
        /// </summary>
        public static float Time => TimerGetElapsed(_clock);

        public static bool IsInitialized => _initialized;

        /// <summary>
        /// If set to true, the library will print debug messages.
        /// Can only be set before the library is initialized.
        /// It's recommended to initialize the library (through its static constructor) by setting this property.
        /// </summary>
        public static bool IsDebugMessagesEnabled
        {
            get => _debugMessagesEnabled;
            set
            {
                if (!_initialized)
                {
                    _debugMessagesEnabled = value;
                }
            }
        }

        internal static gamecache Gamecache => _gamecache;

        /// <summary>
        /// Defines a new variable to store player data in.
        /// </summary>
        public static void DefineValue(string name, Type type, Goal goal = Goal.None, Suggest suggestion = Suggest.None)
        {
            string errorMessage = null;
            if (string.IsNullOrEmpty(name))
            {
                errorMessage = "Variable name is empty.";
            }
            else if (name.Length > 32)
            {
                errorMessage = "Variable name is too long.";
            }
            else if (type == Type.String && goal != Goal.None)
            {
                errorMessage = "Strings must have goal type of none.";
            }
            else if (GetStoredInteger(_gamecache, TypesKey, name) != 0)
            {
                errorMessage = "Variable name is empty.";
            }

            if (errorMessage is null)
            {
                StoreInteger(_gamecache, TypesKey, name, (int)type);
                Emit($"DefVarP {Pack(name)} {EnumNameProvider.GetTypeString(type)} {EnumNameProvider.GetGoalString(goal)} {EnumNameProvider.GetSuggestionString(suggestion)}");
            }
            else if (_debugMessagesEnabled)
            {
                LuaMethods.Print($"{nameof(Mmd)}.{nameof(DefineValue)} Error: {errorMessage}");
            }
        }

        /// <remarks>
        /// Don't try to update a value before defining it.
        /// </remarks>
        public static void UpdateValue(string name, player whichPlayer, Operator @operator, int value)
        {
            UpdateValue(name, whichPlayer, @operator, $"{value}", Type.Int);
        }

        /// <remarks>
        /// Don't try to update a value before defining it.
        /// </remarks>
        public static void UpdateValue(string name, player whichPlayer, Operator @operator, float value)
        {
            UpdateValue(name, whichPlayer, @operator, $"{value}", Type.Float);
        }

        /// <remarks>
        /// Don't try to update a value before defining it.
        /// </remarks>
        public static void UpdateValue(string name, player whichPlayer, string value)
        {
            UpdateValue(name, whichPlayer, Operator.Set, $"\"{Pack(value)}\"", Type.String);
        }

        /// <summary>
        /// Defines an event's arguments and format.
        /// </summary>
        public static void DefineEvent(string name, string format, params string[] args)
        {
            if (GetStoredInteger(_gamecache, EventsKey, name) == 0)
            {
                var numArgs = args.Length;
                StoreInteger(_gamecache, EventsKey, name, numArgs + 1);
                Emit($"DefEvent {Pack(name)} {numArgs} {Aggregate(args)}{(numArgs == 0 ? string.Empty : " ")}{Pack(format)}");
            }
            else if (_debugMessagesEnabled)
            {
                LuaMethods.Print($"{nameof(Mmd)}.{nameof(DefineEvent)} Error: Event redefined.");
            }
        }

        /// <summary>
        /// An event's format string uses {#} to represent arguments
        /// </summary>
        public static void LogEvent(string name, params string[] args)
        {
            var storedValue = GetStoredInteger(_gamecache, EventsKey, name);
            var numArgs = args.Length;
            if (storedValue == numArgs + 1)
            {
                Emit($"Event {Pack(name)}{(numArgs == 0 ? string.Empty : " ")}{Aggregate(args)}");
            }
            else if (_debugMessagesEnabled)
            {
                LuaMethods.Print($"{nameof(Mmd)}.{nameof(LogEvent)} Error: {(storedValue == 0 ? "Event not defined." : $"Event defined with different # of args. [{storedValue}]")}");
            }
        }

        /// <summary>
        /// Sets a player flag.
        /// </summary>
        public static void FlagPlayer(player whichPlayer, Flag flag)
        {
            if (whichPlayer != null)
            {
                var id = GetPlayerId(whichPlayer);
                if (id >= 0 && id < Blizzard.MaxPlayerSlots)
                {
                    if (GetPlayerController(whichPlayer) == MAP_CONTROL_USER)
                    {
                        Emit($"FlagP {id} {EnumNameProvider.GetFlagString(flag)}");
                    }
                }
                else if (_debugMessagesEnabled)
                {
                    LuaMethods.Print($"{nameof(Mmd)}.{nameof(FlagPlayer)} Error: Invalid player id.");
                }
            }
            else if (_debugMessagesEnabled)
            {
                LuaMethods.Print($"{nameof(Mmd)}.{nameof(FlagPlayer)} Error: Invalid player.");
            }
        }

        /// <summary>
        /// Emits meta-data which parsers will ignore, unless they are customized to understand it.
        /// </summary>
        public static void LogCustom(string uniqueId, string data)
        {
            Emit($"custom {Pack(uniqueId)} {Pack(data)}");
        }

        private static int PoorHash(string @string, int seed)
        {
            var length = @string.Length;
            var hash = length + seed;
            foreach (var @char in @string)
            {
                hash = (hash * 41) + @char;
            }

            return hash;
        }

        private static bool Init()
        {
            _initialized = true;
            Emit($"init version {MinimumParserVersion} {LibraryVersion}");

            for (var i = 0; i < Blizzard.MaxPlayerSlots; i++)
            {
                var player = Player(i);
                if (IsUserPlaying(player))
                {
                    Emit($"init pid {i} {Pack(GetPlayerName(player))}");
                }
            }

            var t = CreateTrigger();
            TriggerRegisterTimerEvent(t, TickInterval, true);
            TriggerAddCondition(t, Condition(Tick));

            Initialized?.Invoke(null, null);

            return false;
        }

        /// <summary>
        /// Performs tamper checks.
        /// </summary>
        private static bool Tick()
        {
            // Check previously sent messages for tampering.
            // if (_nodes.Count > 0)
            {
                // while (true)
                while (_nodes.Count > 0)
                {
                    if (Time <= _nodes.Peek().Timeout)
                    {
                        break;
                    }

                    using var node = _nodes.Dequeue();

                    var val = KeyVal + node.Key;
                    var chk = KeyChk + node.Key;
                    var send = true;
                    if (!HaveStoredInteger(_gamecache, val, node.Message))
                    {
                        RaiseGuard("message skipping");
                    }
                    else if (!HaveStoredInteger(_gamecache, chk, node.Key))
                    {
                        RaiseGuard("checksum skipping");
                    }
                    else if (GetStoredInteger(_gamecache, val, node.Message) != node.Checksum)
                    {
                        RaiseGuard("message tampering");
                    }
                    else if (GetStoredInteger(_gamecache, chk, node.Key) != node.Checksum)
                    {
                        RaiseGuard("checksum tampering");
                    }
                    else
                    {
                        send = false;
                    }

                    if (send)
                    {
                        node.Send();
                    }

                    if (_nodes.Count == 0)
                    {
                        MessagesProcessed?.Invoke(null, null);

                        // can't 'break' because it's encapsulated in a System.using() ?
                        // break;
                    }
                }
            }

            // Check for future message tampering.
            for (var i = 0; i < 10; i++)
            {
                if (!HaveStoredInteger(_gamecache, KeyChk + _messageCount, $"{_messageCount}"))
                {
                    break;
                }

                RaiseGuard("message insertion");
                Emit("Blank");
            }

            return false;
        }

        /// <summary>
        /// Returns true for a fixed size uniform random subset of players in the game.
        /// </summary>
        private static bool IsEmitter()
        {
            var picks = new int[Blizzard.MaxPlayerSlots];
            var pickFlags = new bool[Blizzard.MaxPlayerSlots];
            var n = 0;

            for (var i = 0; i < Blizzard.MaxPlayerSlots; i++)
            {
                if (IsUserPlaying(Player(i)))
                {
                    if (n < (int)_senders)
                    {
                        picks[n] = i;
                        pickFlags[i] = true;
                    }
                    else
                    {
                        var r = GetRandomInt(0, n);
                        if (r < (int)_senders)
                        {
                            pickFlags[picks[r]] = false;
                            picks[r] = i;
                            pickFlags[i] = true;
                        }
                    }

                    n++;
                }
            }

            return pickFlags[GetPlayerId(GetLocalPlayer())];
        }

        /// <summary>
        /// Places meta-data in the replay and in network traffic.
        /// </summary>
        private static void Emit(string message)
        {
            if (_initialized)
            {
                var id = _messageCount++;
                var node = new Node(id, message, Time + 7f + GetRandomReal(0, 2 + (0.1f * GetPlayerId(GetLocalPlayer()))), PoorHash(message, id));
                _nodes.Enqueue(node);

                if (IsEmitter())
                {
                    _eventQueue.AddTask(node.Send, Priority.Medium);
                }
            }
            else if (_debugMessagesEnabled)
            {
                LuaMethods.Print($"{nameof(Mmd)}.{nameof(Emit)} Error: Library not initialized yet.");
            }
        }

        /// <summary>
        /// Escapes <see cref="EscapedCharacters"/> in the given string.
        /// </summary>
        private static string Pack(string value)
        {
            var result = string.Empty;
            foreach (var @char in value)
            {
                var current = $"{@char}";
                foreach (var escapeChar in EscapedCharacters)
                {
                    if (@char == escapeChar)
                    {
                        current = $@"\{@char}";
                        break;
                    }
                }

                result += current;
            }

            return result;
        }

        /// <summary>
        /// Triggered when tampering is detected. Increases the number of safeguards against tampering.
        /// </summary>
        private static void RaiseGuard(string reason)
        {
            _senders = Senders.Safe;
            if (_debugMessagesEnabled)
            {
                LuaMethods.Print($"{nameof(Mmd)}: Guard Raised! ({reason})");
            }
        }

        private static void UpdateValue(string name, player whichPlayer, Operator @operator, string value, Type type)
        {
            string errorMessage = null;
            if (whichPlayer is null)
            {
                errorMessage = "Invalid player.";
            }
            else
            {
                var id = GetPlayerId(whichPlayer);
                if (id < 0 || id >= Blizzard.MaxPlayerSlots)
                {
                    errorMessage = "Invalid player id.";
                }
                else if (string.IsNullOrEmpty(name))
                {
                    errorMessage = "Variable name is empty.";
                }
                else if ((int)type != GetStoredInteger(_gamecache, TypesKey, name))
                {
                    errorMessage = "Updated value of undefined variable or used value of incorrect type.";
                }
                else if (name.Length > 50)
                {
                    errorMessage = "Variable name is too long.";
                }

                if (errorMessage is null)
                {
                    Emit($"VarP {id} {Pack(name)} {EnumNameProvider.GetOperatorString(@operator)} {value}");
                }
                else if (_debugMessagesEnabled)
                {
                    LuaMethods.Print($"{nameof(Mmd)}.{nameof(UpdateValue)} Error: {errorMessage}");
                }
            }
        }

        private static bool IsUserPlaying(player whichPlayer)
        {
            return GetPlayerSlotState(whichPlayer) == PLAYER_SLOT_STATE_PLAYING && GetPlayerController(whichPlayer) == MAP_CONTROL_USER;
        }

        private static string Aggregate(params string[] strings)
        {
            return strings.Select(@string => Pack(@string)).Aggregate((accum, next) => $"{accum} {next}");
        }
    }
}