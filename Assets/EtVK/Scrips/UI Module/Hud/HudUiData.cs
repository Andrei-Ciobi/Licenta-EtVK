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
        [SerializeField] private InteractUiEvent interactEvent;
        [SerializeField] private StringEvent errorEvent;
        [SerializeField] private AbilityUiEvent initializeAbilityEvent;
        [SerializeField] private UpgradesUiEvent displayUpgradesEvent;
        [SerializeField] private UpgradeEvent selectUpgradeEvent;
        
        public FloatEvent HealthEvent => healthEvent;
        public FloatEvent StaminaEvent => staminaEvent;
        public InteractUiEvent InteractEvent => interactEvent;
        public StringEvent ErrorEvent => errorEvent;
        public AbilityUiEvent InitializeAbilityEvent => initializeAbilityEvent;
        public UpgradesUiEvent DisplayUpgradesEvent => displayUpgradesEvent;
        public UpgradeEvent SelectUpgradeEvent => selectUpgradeEvent;
    }
}