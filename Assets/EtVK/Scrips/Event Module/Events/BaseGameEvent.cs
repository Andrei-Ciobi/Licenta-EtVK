using System.Collections.Generic;
using EtVK.Scrips.Event_Module.Listeners;
using UnityEngine;

namespace EtVK.Scrips.Event_Module.Events
{
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

        public void Invoke(T item)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventInvoked(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}
