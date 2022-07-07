using EtVK.Scrips.Items_Module.Weapons_Module;
using UnityEngine;

namespace EtVK.Scrips.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "ScriptableObjects/Events/WeaponEvent")]
    public class WeaponEvent : BaseGameEvent<Weapon> { }
}