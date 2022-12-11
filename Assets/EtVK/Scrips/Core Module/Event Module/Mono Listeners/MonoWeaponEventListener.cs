using EtVK.Event_Module.Core;
using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Events;
using EtVK.Items_Module.Weapons;

namespace EtVK.Event_Module.Mono_Listeners
{
    public class MonoWeaponEventListener : BaseMonoEventListener<Weapon, WeaponEvent, CustomWeaponEvent> { }
}