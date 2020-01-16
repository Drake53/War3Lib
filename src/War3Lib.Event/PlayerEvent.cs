using System;
using System.Collections.Generic;

using static War3Api.Common;

namespace War3Lib.Event
{
    public sealed class PlayerEvent
    {
        private static readonly TriggerEventWrapper<PlayerEventArgs> _anyPlayerLeave = new TriggerEventWrapper<PlayerEventArgs>();
        private static readonly TriggerEventWrapper<PlayerChatEventArgs> _anyPlayerChat = new TriggerEventWrapper<PlayerChatEventArgs>();

        private static readonly Dictionary<player, PlayerEvent> _dict = new Dictionary<player, PlayerEvent>();

        private readonly TriggerEventWrapper<PlayerEventArgs> _playerLeave = new TriggerEventWrapper<PlayerEventArgs>();
        private readonly TriggerEventWrapper<PlayerChatEventArgs> _playerChat = new TriggerEventWrapper<PlayerChatEventArgs>();

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

        private static @event PlayerLeaveEvent(trigger trigger, player player)
        {
            return TriggerRegisterPlayerEvent(trigger, player, EVENT_PLAYER_LEAVE);
        }

        private static @event PlayerChatEvent(trigger trigger, player player)
        {
            return TriggerRegisterPlayerChatEvent(trigger, player, string.Empty, false);
        }

        private static PlayerEventArgs GetPlayerLeaveEventResponse()
        {
            return new PlayerEventArgs(GetTriggerPlayer());
        }

        private static PlayerChatEventArgs GetPlayerChatEventResponse()
        {
            return new PlayerChatEventArgs(GetTriggerPlayer(), GetEventPlayerChatString());
        }

        private static void Add<TEventArgs>(TriggerEventWrapper<TEventArgs> wrapper, EventHandler<TEventArgs> handler, Func<trigger, player, @event> registrar, Func<TEventArgs> responder)
            where TEventArgs : EventArgs
        {
            if (wrapper.IsNull)
            {
                var events = new List<Func<trigger, @event>>();
                for (var i = 0; i < GetBJMaxPlayers(); i++)
                {
                    var player = Player(i);
                    events.Add((trigger) => registrar(trigger, player));
                }

                wrapper.CreateWrapper(responder, events);
            }

            wrapper.Event += handler;
        }

        private void AddOne<TEventArgs>(TriggerEventWrapper<TEventArgs> wrapper, EventHandler<TEventArgs> handler, Func<trigger, player, @event> registrar, Func<TEventArgs> responder)
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