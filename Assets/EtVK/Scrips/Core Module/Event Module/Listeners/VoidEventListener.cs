using EtVK.Event_Module.Core;
using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;

namespace EtVK.Event_Module.Listeners
{
    public class VoidEventListener : BaseEventListener<Void, VoidEvent, CustomVoidEvent>
    {
        public VoidEventListener(VoidEvent voidEvent) : base(voidEvent, new CustomVoidEvent())
        {
            
        }
    }
}