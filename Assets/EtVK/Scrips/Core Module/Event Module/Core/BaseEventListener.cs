using UnityEngine.Events;

namespace EtVK.Event_Module.Core
{
    public class BaseEventListener<T, E, UER> : IGameEventListener<T> where E : BaseGameEvent<T> 
        where UER : UnityEvent<T>
    {
        private E gameEvent;
        private UER eventResponse;
        
        protected BaseEventListener(E gameEvent, UER eventResponse)
        {
            this.gameEvent = gameEvent;
            this.eventResponse = eventResponse;
            
            gameEvent.RegisterListener(this);
        }

        ~BaseEventListener()
        {
            gameEvent.UnregisterListener(this);
        }

        public void AddCallback(UnityAction<T> callback)
        {
            eventResponse.AddListener(callback);
        }

        public void RemoveCallbacks()
        {
            eventResponse.RemoveAllListeners();
        }
        public void OnEventInvoked(T item)
        {
            eventResponse?.Invoke(item);
        }
    }
}