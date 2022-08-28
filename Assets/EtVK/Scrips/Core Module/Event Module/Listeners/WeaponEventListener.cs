using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Events;
using EtVK.Items_Module.Weapons;

namespace EtVK.Event_Module.Listeners
{
    public class WeaponEventListener : BaseGameEventListener<Weapon, WeaponEvent, CustomWeaponEvent> { }
}