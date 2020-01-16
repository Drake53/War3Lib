using System;
using System.Collections.Generic;

using static War3Api.Common;

namespace War3Lib.Event
{
    internal sealed class TriggerEventWrapper<TEventArgs> where TEventArgs : EventArgs
    {
        private trigger _trigger;
        private triggercondition _triggercondition;

        internal bool IsNull => Event is null;

        internal void CreateWrapper(Func<TEventArgs> eventArgsFunc, Func<trigger, @event> @event)
        {
            CreateWrapper(eventArgsFunc, new[] { @event });
        }

        internal void CreateWrapper(Func<TEventArgs> eventArgsFunc, IEnumerable<Func<trigger, @event>> events)
        {
            _trigger = CreateTrigger();
            _triggercondition = TriggerAddCondition(_trigger, Condition(() =>
            {
                Event.Invoke(null, eventArgsFunc());
                return false;
            }));

            foreach (var @event in events)
            {
                @event(_trigger);
            }
        }

        internal void DestroyWrapper()
        {
            TriggerRemoveCondition(_trigger, _triggercondition);
            DestroyTrigger(_trigger);
        }

        internal void RemoveEventHandler(EventHandler<TEventArgs> handler)
        {
            Event -= handler;
            if (IsNull)
            {
                DestroyWrapper();
            }
        }

        internal event EventHandler<TEventArgs> Event;
    }
}