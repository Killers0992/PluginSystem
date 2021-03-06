using PluginSystem.EventHandlers;
using System;
using System.Collections.Generic;

namespace PluginSystem.Events
{
    public enum Priority { Highest = 100, High = 80, Normal = 50, Low = 20, Lowest = 0 };

    public class EventManager
    {
        private static EventManager singleton;

        public static EventManager Manager
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new EventManager();
                }
                return singleton;
            }
        }

        private static PriorityComparator priorityCompare = new PriorityComparator();
        private Dictionary<Type, List<EventHandlerWrapper>> event_meta;
        private readonly Dictionary<IPlugin<IConfig>, Snapshot> snapshots;

        public EventManager()
        {
            event_meta = new Dictionary<Type, List<EventHandlerWrapper>>();
            snapshots = new Dictionary<IPlugin<IConfig>, Snapshot>();
        }


        public void HandleEvent<T>(Event ev) where T : IEventHandler
        {
            foreach (T handler in GetEventHandlers<T>())
            {
                try
                {
                    ev.ExecuteHandler(handler);
                }
                catch (Exception e)
                {
                    PluginManager.Manager.Logger.Error("Event Handler: " + handler.GetType().ToString() + " Failed to handle event:" + ev.GetType().ToString());
                    PluginManager.Manager.Logger.Error(e.ToString());
                }
            }
        }

        public void AddEventHandlers(IPlugin<IConfig> plugin, IEventHandler handler, Priority priority = Priority.Normal)
        {
            foreach (Type intfce in handler.GetType().GetInterfaces())
            {
                if (typeof(IEventHandler).IsAssignableFrom(intfce))
                {
                    AddEventHandler(plugin, intfce, handler, priority);
                }
            }
        }

        public void AddEventHandler(IPlugin<IConfig> plugin, Type eventType, IEventHandler handler, Priority priority = Priority.Normal)
        {
            EventHandlerWrapper wrapper = new EventHandlerWrapper(plugin, priority, handler);

            if (PluginManager.Manager.GetEnabledPlugin(plugin.Details.Id) == null)
            {
                if (!snapshots.ContainsKey(plugin))
                    snapshots.Add(plugin, new Snapshot());

                snapshots[plugin].Entries.Add(new Snapshot.SnapshotEntry(eventType, wrapper));
            }

            AddEventMeta(eventType, wrapper, handler);
        }

        private void AddEventMeta(Type eventType, EventHandlerWrapper wrapper, IEventHandler handler)
        {
            if (!event_meta.ContainsKey(eventType))
            {
                event_meta.Add(eventType, new List<EventHandlerWrapper>
                {
                    wrapper
                });
            }
            else
            {
                List<EventHandlerWrapper> meta = event_meta[eventType];
                meta.Add(wrapper);
                meta.Sort(priorityCompare);
                meta.Reverse();
            }
        }

        public void AddSnapshotEventHandlers(IPlugin<IConfig> plugin)
        {
            if (!snapshots.ContainsKey(plugin) || !snapshots[plugin].Active)
            {
                return;
            }

            snapshots[plugin].Active = false;
            foreach (Snapshot.SnapshotEntry entry in snapshots[plugin].Entries)
            {
                AddEventMeta(entry.Type, entry.Wrapper, entry.Wrapper.Handler);
            }
        }

        public void RemoveEventHandlers(IPlugin<IConfig> plugin)
        {
            Dictionary<Type, List<EventHandlerWrapper>> new_event_meta = new Dictionary<Type, List<EventHandlerWrapper>>();
            foreach (var meta in event_meta)
            {
                List<EventHandlerWrapper> newList = new List<EventHandlerWrapper>();

                foreach (EventHandlerWrapper wrapper in meta.Value)
                {
                    if (wrapper.Plugin != plugin)
                        newList.Add(wrapper);
                }

                if (newList.Count > 0)
                    new_event_meta.Add(meta.Key, newList);
            }

            event_meta = new_event_meta;

            if (snapshots.ContainsKey(plugin))
                snapshots[plugin].Active = true;
        }


        public List<T> GetEventHandlers<T>() where T : IEventHandler
        {
            List<T> events = new List<T>();
            if (event_meta.ContainsKey(typeof(T)))
            {
                foreach (EventHandlerWrapper wrapper in event_meta[typeof(T)])
                {
                    if (wrapper.Handler is T tHandler)
                        events.Add(tHandler);
                }
            }

            return events;
        }

        private class PriorityComparator : IComparer<EventHandlerWrapper>
        {
            public int Compare(EventHandlerWrapper x, EventHandlerWrapper y)
            {
                return x.Priority.CompareTo(y.Priority);
            }
        }

        private class Snapshot
        {
            public List<SnapshotEntry> Entries { get; private set; }
            public bool Active { get; set; }

            public Snapshot()
            {
                Entries = new List<SnapshotEntry>();
            }

            public class SnapshotEntry
            {
                public Type Type { get; }
                public EventHandlerWrapper Wrapper { get; }

                public SnapshotEntry(Type type, EventHandlerWrapper wrapper)
                {
                    Type = type;
                    Wrapper = wrapper;
                }
            }
        }
    }

    public class EventHandlerWrapper
    {
        public Priority Priority { get; }
        public IEventHandler Handler { get; }
        public IPlugin<IConfig> Plugin { get; }

        public EventHandlerWrapper(IPlugin<IConfig> plugin, Priority priority, IEventHandler handler)
        {
            this.Plugin = plugin;
            this.Priority = priority;
            this.Handler = handler;
        }
    }
}
