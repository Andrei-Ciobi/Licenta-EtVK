using EtVK.Event_Module.Core;
using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;

namespace EtVK.Event_Module.Listeners
{
    public class UpgradesUiEventListener : BaseEventListener<UpgradesUiData, UpgradesUiEvent, CustomUpgradesUiEvent>
    {
        public UpgradesUiEventListener(UpgradesUiEvent gameEvent) : base(gameEvent, new CustomUpgradesUiEvent())
        {
        }
    }
}