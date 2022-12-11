using EtVK.Event_Module.Core;
using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Events;

namespace EtVK.Event_Module.Listeners
{
    public class BoolEventListener : BaseEventListener<bool, BoolEvent, CustomBoolEvent>
    {
        public BoolEventListener(BoolEvent gameEvent) : base(gameEvent, new CustomBoolEvent())
        {
            
        }
    }
}