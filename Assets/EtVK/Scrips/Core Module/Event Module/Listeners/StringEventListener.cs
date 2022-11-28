using EtVK.Event_Module.Core;
using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Events;

namespace EtVK.Event_Module.Listeners
{
    public class StringEventListener : BaseEventListener<string, StringEvent, CustomStringEvent>
    {
        public StringEventListener(StringEvent gameEvent) : base(gameEvent, new CustomStringEvent())
        {
        }
    }
}