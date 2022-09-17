namespace EtVK.Save_System_Module
{
    public interface ISavable
    {
        public object SaveState();
        public void LoadState(object state);
    }
}