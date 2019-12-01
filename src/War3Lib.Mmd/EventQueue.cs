using System;
using System.Collections.Generic;

using static War3Api.Common;

namespace War3Lib.Mmd
{
    internal class EventQueue
    {
        private const float TickInterval = 0.10f;
        private const int MaxEventsPerTick = 2;

        private readonly Queue<Func<bool>> _lowPriority;
        private readonly Queue<Func<bool>> _mediumPriority;
        private readonly Queue<Func<bool>> _highPriority;

        private Func<bool> _currentTask;

        public EventQueue()
        {
            _lowPriority = new Queue<Func<bool>>();
            _mediumPriority = new Queue<Func<bool>>();
            _highPriority = new Queue<Func<bool>>();

            var t = CreateTrigger();
            TriggerRegisterTimerEvent(t, TickInterval, true);
            TriggerAddCondition(t, Condition(Tick));
        }

        public void AddTask(Func<bool> task, Priority priority)
        {
            (priority switch
            {
                Priority.High => _highPriority,
                Priority.Medium => _mediumPriority,
                Priority.Low => _lowPriority,

                _ => null,
            })?.Enqueue(task);
        }

        private bool Tick()
        {
            if (_currentTask != null)
            {
                for (var i = 0; i < MaxEventsPerTick; i++)
                {
                    if (_currentTask != null)
                    {
                        if (_currentTask.Invoke())
                        {
                            _currentTask = null;
                        }
                    }
                    else
                    {
                        UpdateCurrentTask();
                    }
                }
            }
            else
            {
                UpdateCurrentTask();
            }

            return false;
        }

        private void UpdateCurrentTask()
        {
            if (_highPriority.Count > 0)
            {
                _currentTask = _highPriority.Dequeue();
            }
            else if (_mediumPriority.Count > 0)
            {
                _currentTask = _mediumPriority.Dequeue();
            }
            else if (_lowPriority.Count > 0)
            {
                _currentTask = _lowPriority.Dequeue();
            }
        }
    }
}