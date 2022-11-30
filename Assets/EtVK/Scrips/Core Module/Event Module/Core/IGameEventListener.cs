namespace EtVK.Event_Module.Core
{
    public interface IGameEventListener<T>
    {
        void OnEventInvoked(T item);
    }
}
