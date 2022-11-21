using EtVK.Event_Module.Core;
using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Events;
using UnityEngine.Events;

namespace EtVK.Event_Module.Listeners
{
    public class FloatEventListener : BaseEventListener<float, FloatEvent, CustomFloatEvent>
    {
        public FloatEventListener(FloatEvent floatEvent, CustomFloatEvent eventResponse)
        : base(floatEvent, eventResponse)
        {
            
        }
    }
}