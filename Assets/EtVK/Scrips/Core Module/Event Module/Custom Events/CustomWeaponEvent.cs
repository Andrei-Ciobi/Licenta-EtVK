using EtVK.Items_Module.Weapons;
using UnityEngine.Events;

namespace EtVK.Event_Module.Custom_Events
{
    [System.Serializable] public class CustomWeaponEvent : UnityEvent<Weapon> { }
}