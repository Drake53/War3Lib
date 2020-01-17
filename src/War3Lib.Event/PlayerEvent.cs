using System;
using System.Collections.Generic;

using static War3Api.Common;

namespace War3Lib.Event
{
    public sealed class PlayerEvent
    {
        private static readonly TriggerEventWrapper<PlayerEventArgs> _anyPlayerLeave = new TriggerEventWrapper<PlayerEventArgs>();
        private static readonly TriggerEventWrapper<PlayerChatEventArgs> _anyPlayerChat = new TriggerEventWrapper<PlayerChatEventArgs>();
        private static readonly TriggerEventWrapper<PlayerKeyEventArgs> _anyPlayerKey = new TriggerEventWrapper<PlayerKeyEventArgs>();

        private static readonly Dictionary<player, PlayerEvent> _dict = new Dictionary<player, PlayerEvent>();

        private readonly TriggerEventWrapper<PlayerEventArgs> _playerLeave = new TriggerEventWrapper<PlayerEventArgs>();
        private readonly TriggerEventWrapper<PlayerChatEventArgs> _playerChat = new TriggerEventWrapper<PlayerChatEventArgs>();
        private readonly TriggerEventWrapper<PlayerKeyEventArgs> _playerKey = new TriggerEventWrapper<PlayerKeyEventArgs>();

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

        public static event EventHandler<PlayerKeyEventArgs> AnyPlayerKey
        {
            add => Add(_anyPlayerKey, value, PlayerKeyEvent, GetPlayerKeyEventResponse);
            remove => _anyPlayerKey.RemoveEventHandler(value);
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

        public event EventHandler<PlayerKeyEventArgs> PlayerKey
        {
            add => AddOne(_playerKey, value, PlayerKeyEvent, GetPlayerKeyEventResponse);
            remove => _playerKey.RemoveEventHandler(value);
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

        private static void PlayerKeyEvent(trigger trigger, player player)
        {
            for (var keyType = 8; keyType <= 255; keyType++)
            {
                for (var metaKey = 0; metaKey <= 15; metaKey++)
                {
                    BlzTriggerRegisterPlayerKeyEvent(trigger, player, ConvertOsKeyType(keyType), metaKey, true);
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

        private static PlayerKeyEventArgs GetPlayerKeyEventResponse()
        {
            return new PlayerKeyEventArgs(GetTriggerPlayer(), BlzGetTriggerPlayerKey(), BlzGetTriggerPlayerMetaKey(), BlzGetTriggerPlayerIsKeyDown());
        }

        private static void Add<TEventArgs>(TriggerEventWrapper<TEventArgs> wrapper, EventHandler<TEventArgs> handler, Action<trigger, player> registrar, Func<TEventArgs> responder)
            where TEventArgs : EventArgs
        {
            if (wrapper.IsNull)
            {
                var events = new List<Action<trigger>>();
                for (var i = 0; i < GetBJMaxPlayers(); i++)
                {
                    var player = Player(i);
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