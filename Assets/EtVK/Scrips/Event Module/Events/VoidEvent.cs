
using EtVK.Scrips.Event_Module.Event_Types;
using UnityEngine;

namespace EtVK.Scrips.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName ="ScriptableObjects/Events/VoidEvent")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Invoke() => Invoke(new Void());
    }
}
