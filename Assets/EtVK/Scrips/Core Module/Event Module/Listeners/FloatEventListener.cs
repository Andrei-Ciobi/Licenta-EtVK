using EtVK.Event_Module.Core;
using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Events;

namespace EtVK.Event_Module.Listeners
{
    public class FloatEventListener : BaseEventListener<float, FloatEvent, CustomFloatEvent>
    {
        public FloatEventListener(FloatEvent floatEvent)
        : base(floatEvent, new CustomFloatEvent())
        {
            
        }
    }
}