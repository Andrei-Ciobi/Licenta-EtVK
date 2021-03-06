using EtVK.Scrips.Event_Module.Events;
using UnityEngine;
using UnityEngine.Events;

namespace EtVK.Scrips.Event_Module.Listeners
{
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
        IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField]
        private E gameEvent;
        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField]
        private UER unityEventResponse;

        private void OnEnable()
        {
            if(gameEvent == null)
            {
                return;
            }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if(gameEvent == null)
            {
                return;
            }

            GameEvent.UnregisterListener(this);
        }

        public void OnEventInvoked(T item)
        {
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(item);
            }
        }
    }
}
