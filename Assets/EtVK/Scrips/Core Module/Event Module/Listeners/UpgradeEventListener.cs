using EtVK.Event_Module.Core;
using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Events;
using EtVK.Upgrades_Module.Core;

namespace EtVK.Event_Module.Listeners
{
    public class UpgradeEventListener : BaseEventListener<BaseUpgradeData, UpgradeEvent, CustomUpgradeEvent>
    {
        protected UpgradeEventListener(UpgradeEvent gameEvent) : base(gameEvent, new CustomUpgradeEvent())
        {
        }
    }
}