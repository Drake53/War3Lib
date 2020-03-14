using System;
using System.Collections.Generic;

using War3Lib.Event.Player;

using static War3Api.Common;

namespace War3Lib.Event
{
    public sealed class PlayerEvent
    {
        /*
    constant playerevent EVENT_PLAYER_STATE_LIMIT               = ConvertPlayerEvent(11)
        constant native GetEventPlayerState takes nothing returns playerstate
    constant playerevent EVENT_PLAYER_ALLIANCE_CHANGED          = ConvertPlayerEvent(12)

    constant playerevent EVENT_PLAYER_DEFEAT                    = ConvertPlayerEvent(13)
        constant native GetTriggerPlayer takes nothing returns player
    constant playerevent EVENT_PLAYER_VICTORY                   = ConvertPlayerEvent(14)
        constant native GetTriggerPlayer takes nothing returns player
    constant playerevent EVENT_PLAYER_LEAVE                     = ConvertPlayerEvent(15)
    constant playerevent EVENT_PLAYER_CHAT                      = ConvertPlayerEvent(16)
    constant playerevent EVENT_PLAYER_END_CINEMATIC             = ConvertPlayerEvent(17)

    constant playerevent        EVENT_PLAYER_ARROW_LEFT_DOWN            = ConvertPlayerEvent(261)
    constant playerevent        EVENT_PLAYER_ARROW_LEFT_UP              = ConvertPlayerEvent(262)
    constant playerevent        EVENT_PLAYER_ARROW_RIGHT_DOWN           = ConvertPlayerEvent(263)
    constant playerevent        EVENT_PLAYER_ARROW_RIGHT_UP             = ConvertPlayerEvent(264)
    constant playerevent        EVENT_PLAYER_ARROW_DOWN_DOWN            = ConvertPlayerEvent(265)
    constant playerevent        EVENT_PLAYER_ARROW_DOWN_UP              = ConvertPlayerEvent(266)
    constant playerevent        EVENT_PLAYER_ARROW_UP_DOWN              = ConvertPlayerEvent(267)
    constant playerevent        EVENT_PLAYER_ARROW_UP_UP                = ConvertPlayerEvent(268)
    constant playerevent        EVENT_PLAYER_MOUSE_DOWN                 = ConvertPlayerEvent(305)
    constant playerevent        EVENT_PLAYER_MOUSE_UP                   = ConvertPlayerEvent(306)
    constant playerevent        EVENT_PLAYER_MOUSE_MOVE                 = ConvertPlayerEvent(307)
    constant playerevent        EVENT_PLAYER_SYNC_DATA                  = ConvertPlayerEvent(309)
    constant playerevent        EVENT_PLAYER_KEY                        = ConvertPlayerEvent(311)
    constant playerevent        EVENT_PLAYER_KEY_DOWN                   = ConvertPlayerEvent(312)
    constant playerevent        EVENT_PLAYER_KEY_UP                     = ConvertPlayerEvent(313)
    */

        // native TriggerRegisterPlayerAllianceChange takes trigger whichTrigger, player whichPlayer, alliancetype whichAlliance returns event
        // native TriggerRegisterPlayerStateEvent takes trigger whichTrigger, player whichPlayer, playerstate whichState, limitop opcode, real limitval returns event
            // constant playerstate PLAYER_STATE_GAME_RESULT               = ConvertPlayerState(0)

            // current resource levels
            // constant playerstate PLAYER_STATE_RESOURCE_GOLD             = ConvertPlayerState(1)
            // constant playerstate PLAYER_STATE_RESOURCE_LUMBER           = ConvertPlayerState(2)
            // constant playerstate PLAYER_STATE_RESOURCE_HERO_TOKENS      = ConvertPlayerState(3)
            // constant playerstate PLAYER_STATE_RESOURCE_FOOD_CAP         = ConvertPlayerState(4)
            // constant playerstate PLAYER_STATE_RESOURCE_FOOD_USED        = ConvertPlayerState(5)
            // constant playerstate PLAYER_STATE_FOOD_CAP_CEILING          = ConvertPlayerState(6)

            // constant playerstate PLAYER_STATE_GIVES_BOUNTY              = ConvertPlayerState(7)
            // constant playerstate PLAYER_STATE_ALLIED_VICTORY            = ConvertPlayerState(8)
            // constant playerstate PLAYER_STATE_PLACED                    = ConvertPlayerState(9)
            // constant playerstate PLAYER_STATE_OBSERVER_ON_DEATH         = ConvertPlayerState(10)
            // constant playerstate PLAYER_STATE_OBSERVER                  = ConvertPlayerState(11)
            // constant playerstate PLAYER_STATE_UNFOLLOWABLE              = ConvertPlayerState(12)

            // taxation rate for each resource
            // constant playerstate PLAYER_STATE_GOLD_UPKEEP_RATE          = ConvertPlayerState(13)
            // constant playerstate PLAYER_STATE_LUMBER_UPKEEP_RATE        = ConvertPlayerState(14)

            // cumulative resources collected by the player during the mission
            // constant playerstate PLAYER_STATE_GOLD_GATHERED             = ConvertPlayerState(15)
            // constant playerstate PLAYER_STATE_LUMBER_GATHERED           = ConvertPlayerState(16)

            // constant playerstate PLAYER_STATE_NO_CREEP_SLEEP            = ConvertPlayerState(25)
        // native BlzTriggerRegisterPlayerSyncEvent           takes trigger whichTrigger, player whichPlayer, string prefix, boolean fromServer returns event
            // native BlzGetTriggerSyncPrefix                     takes nothing returns string
            // native BlzGetTriggerSyncData                       takes nothing returns string
            
            // native BlzGetTriggerPlayerMouseX                   takes nothing returns real
            // native BlzGetTriggerPlayerMouseY                   takes nothing returns real
            // native BlzGetTriggerPlayerMousePosition            takes nothing returns location
            // native BlzGetTriggerPlayerMouseButton              takes nothing returns mousebuttontype



        private static readonly TriggerEventWrapper<PlayerEventArgs> _anyPlayerLeave = new TriggerEventWrapper<PlayerEventArgs>();
        private static readonly TriggerEventWrapper<PlayerChatEventArgs> _anyPlayerChat = new TriggerEventWrapper<PlayerChatEventArgs>();
        private static readonly TriggerEventWrapper<PlayerKeyEventArgs> _anyPlayerKeyDown = new TriggerEventWrapper<PlayerKeyEventArgs>();
        private static readonly TriggerEventWrapper<PlayerKeyEventArgs> _anyPlayerKeyUp = new TriggerEventWrapper<PlayerKeyEventArgs>();

        private static readonly Dictionary<player, PlayerEvent> _dict = new Dictionary<player, PlayerEvent>();

        private readonly TriggerEventWrapper<PlayerEventArgs> _playerLeave = new TriggerEventWrapper<PlayerEventArgs>();
        private readonly TriggerEventWrapper<PlayerChatEventArgs> _playerChat = new TriggerEventWrapper<PlayerChatEventArgs>();
        private readonly TriggerEventWrapper<PlayerKeyEventArgs> _playerKeyDown = new TriggerEventWrapper<PlayerKeyEventArgs>();
        private readonly TriggerEventWrapper<PlayerKeyEventArgs> _playerKeyUp = new TriggerEventWrapper<PlayerKeyEventArgs>();

        private readonly player _whichPlayer;

        private PlayerEvent(player whichPlayer)
        {
            _whichPlayer = whichPlayer;
        }

        public static event EventHandler<PlayerEventArgs> AnyPlayerLeave
        {
            add => Add(_anyPlayerLeave, value, PlayerLeaveEvent, GetPlayerLeaveEventResponse);
            remove => _anyPlayerLeave.RemoveEventHandler(value);
        }

        public static event EventHandler<PlayerChatEventArgs> AnyPlayerChat
        {
            add => Add(_anyPlayerChat, value, PlayerChatEvent, GetPlayerChatEventResponse);
            remove => _anyPlayerChat.RemoveEventHandler(value);
        }

        public static event EventHandler<PlayerKeyEventArgs> AnyPlayerKeyDown
        {
            add => Add(_anyPlayerKeyDown, value, PlayerKeyDownEvent, GetPlayerKeyDownEventResponse);
            remove => _anyPlayerKeyDown.RemoveEventHandler(value);
        }

        public static event EventHandler<PlayerKeyEventArgs> AnyPlayerKeyUp
        {
            add => Add(_anyPlayerKeyUp, value, PlayerKeyUpEvent, GetPlayerKeyUpEventResponse);
            remove => _anyPlayerKeyUp.RemoveEventHandler(value);
        }

        public static event EventHandler<PlayerKeyEventArgs> AnyPlayerKey
        {
            add
            {
                AnyPlayerKeyDown += value;
                AnyPlayerKeyUp += value;
            }
            remove
            {
                AnyPlayerKeyDown -= value;
                AnyPlayerKeyUp -= value;
            }
        }

        public event EventHandler<PlayerEventArgs> PlayerLeave
        {
            add => AddOne(_playerLeave, value, PlayerLeaveEvent, GetPlayerLeaveEventResponse);
            remove => _playerLeave.RemoveEventHandler(value);
        }

        public event EventHandler<PlayerChatEventArgs> PlayerChat
        {
            add => AddOne(_playerChat, value, PlayerChatEvent, GetPlayerChatEventResponse);
            remove => _playerChat.RemoveEventHandler(value);
        }

        public event EventHandler<PlayerKeyEventArgs> PlayerKeyDown
        {
            add => AddOne(_playerKeyDown, value, PlayerKeyDownEvent, GetPlayerKeyDownEventResponse);
            remove => _playerKeyDown.RemoveEventHandler(value);
        }

        public event EventHandler<PlayerKeyEventArgs> PlayerKeyUp
        {
            add => AddOne(_playerKeyUp, value, PlayerKeyUpEvent, GetPlayerKeyUpEventResponse);
            remove => _playerKeyUp.RemoveEventHandler(value);
        }

        public event EventHandler<PlayerKeyEventArgs> PlayerKey
        {
            add
            {
                PlayerKeyDown += value;
                PlayerKeyUp += value;
            }
            remove
            {
                PlayerKeyDown -= value;
                PlayerKeyUp -= value;
            }
        }

        public static PlayerEvent GetPlayerEvent(player whichPlayer)
        {
            if (_dict.TryGetValue(whichPlayer, out var playerEvent))
            {
                return playerEvent;
            }

            playerEvent = new PlayerEvent(whichPlayer);
            _dict[whichPlayer] = playerEvent;
            return playerEvent;
        }

        private static void PlayerLeaveEvent(trigger trigger, player player)
        {
            TriggerRegisterPlayerEvent(trigger, player, EVENT_PLAYER_LEAVE);
        }

        private static void PlayerChatEvent(trigger trigger, player player)
        {
            TriggerRegisterPlayerChatEvent(trigger, player, string.Empty, false);
        }

        private static void PlayerKeyDownEvent(trigger trigger, player player)
        {
            for (var keyType = 8; keyType <= 255; keyType++)
            {
                for (var metaKey = 0; metaKey <= 15; metaKey++)
                {
                    BlzTriggerRegisterPlayerKeyEvent(trigger, player, ConvertOsKeyType(keyType), metaKey, true);
                }
            }
        }

        private static void PlayerKeyUpEvent(trigger trigger, player player)
        {
            for (var keyType = 8; keyType <= 255; keyType++)
            {
                for (var metaKey = 0; metaKey <= 15; metaKey++)
                {
                    BlzTriggerRegisterPlayerKeyEvent(trigger, player, ConvertOsKeyType(keyType), metaKey, false);
                }
            }
        }

        private static PlayerEventArgs GetPlayerLeaveEventResponse()
        {
            return new PlayerEventArgs(GetTriggerPlayer());
        }

        private static PlayerChatEventArgs GetPlayerChatEventResponse()
        {
            return new PlayerChatEventArgs(GetTriggerPlayer(), GetEventPlayerChatString());
        }

        private static PlayerKeyEventArgs GetPlayerKeyDownEventResponse()
        {
            return new PlayerKeyEventArgs(GetTriggerPlayer(), BlzGetTriggerPlayerKey(), BlzGetTriggerPlayerMetaKey(), true);
        }

        private static PlayerKeyEventArgs GetPlayerKeyUpEventResponse()
        {
            return new PlayerKeyEventArgs(GetTriggerPlayer(), BlzGetTriggerPlayerKey(), BlzGetTriggerPlayerMetaKey(), false);
        }

        private static void Add<TEventArgs>(TriggerEventWrapper<TEventArgs> wrapper, EventHandler<TEventArgs> handler, Action<trigger, player> registrar, Func<TEventArgs> responder)
            where TEventArgs : EventArgs
        {
            if (wrapper.IsNull)
            {
                var events = new List<Action<trigger>>();
                for (var i = 0; i < GetBJMaxPlayers(); i++)
                {
                    var player = War3Api.Common.Player(i);
                    events.Add((trigger) => registrar(trigger, player));
                }

                wrapper.CreateWrapper(responder, events);
            }

            wrapper.Event += handler;
        }

        private void AddOne<TEventArgs>(TriggerEventWrapper<TEventArgs> wrapper, EventHandler<TEventArgs> handler, Action<trigger, player> registrar, Func<TEventArgs> responder)
            where TEventArgs : EventArgs
        {
            if (wrapper.IsNull)
            {
                wrapper.CreateWrapper(responder, (trigger) => registrar(trigger, _whichPlayer));
            }

            wrapper.Event += handler;
        }
    }
}