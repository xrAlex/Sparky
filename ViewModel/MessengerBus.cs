using System;
using System.Collections.Generic;

namespace ViewModel
{
    public class Messenger
    {
        public static Messenger Default { get; } = new Messenger();

        protected readonly Dictionary<Type, List<Delegate>?> Actions = new Dictionary<Type, List<Delegate>?>();

        public void Register<T>(Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            lock (Actions)
            {
                var type = typeof(T);
                if (Actions.TryGetValue(type, out var list))
                {
                    if (!list.Contains(action))
                        list.Add(action);
                }
                else
                {
                    Actions.Add(type, new List<Delegate>(1) { action });
                }
            }
        }

        public void Unregister<T>(Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            lock (Actions)
            {
                var type = typeof(T);
                if (Actions.TryGetValue(type, out var list))
                {
                    list?.RemoveAll(act => (Action<T>)act == action);
                }
            }
        }

        public void Send<T>(T message)
        {
            lock (Actions)
            {
                var type = typeof(T);
                if (Actions.TryGetValue(type, out var list))
                {
                    list?.ForEach(action => ((Action<T>)action)(message));
                }
            }
        }
    }
}
