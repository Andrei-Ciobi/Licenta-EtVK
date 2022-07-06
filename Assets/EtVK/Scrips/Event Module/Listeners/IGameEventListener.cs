namespace EtVK.Scrips.Event_Module.Listeners
{
    public interface IGameEventListener<T>
    {
        void OnEventInvoked(T item);
    }
}
