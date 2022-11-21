using EtVK.Event_Module.Events;
using EtVK.UI_Module.Core;
using UnityEngine;

namespace EtVK.UI_Module.Hud
{
    [CreateAssetMenu(menuName = "ScriptableObjects/UiData/Hud")]
    public class HudUiData : BaseUiData
    {
        [SerializeField] private FloatEvent healthEvent;
        [SerializeField] private FloatEvent staminaEvent;
        
        public FloatEvent HealthEvent => healthEvent;
        public FloatEvent StaminaEvent => staminaEvent;
    }
}