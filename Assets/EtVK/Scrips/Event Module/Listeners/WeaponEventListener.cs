using EtVK.Scrips.Event_Module.Custom_Events;
using EtVK.Scrips.Event_Module.Events;
using EtVK.Scrips.Items_Module.Weapons_Module;

namespace EtVK.Scrips.Event_Module.Listeners
{
    public class WeaponEventListener : BaseGameEventListener<Weapon, WeaponEvent, CustomWeaponEvent> { }
}