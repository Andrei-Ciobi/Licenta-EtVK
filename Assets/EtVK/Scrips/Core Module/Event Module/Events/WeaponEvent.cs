using EtVK.Event_Module.Core;
using EtVK.Items_Module.Weapons;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "ScriptableObjects/Events/WeaponEvent")]
    public class WeaponEvent : BaseGameEvent<Weapon> { }
}