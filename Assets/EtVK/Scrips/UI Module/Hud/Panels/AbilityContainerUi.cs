using System.Collections.Generic;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Listeners;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Hud.Panels
{
    public class AbilityContainerUi : BasePanel<HudManager>
    {
        private AbilityUiEventListener initializeAbilityListener;
        public new class UxmlFactory : UxmlFactory<AbilityContainerUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : BasePanel<HudManager>.UxmlTraits
        {
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as AbilityContainerUi;
                
                ate.Initialize();
            }

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription { get{yield break;} }
        }

        private void BindAbilityToUi(AbilityUiData abilityUiData)
        {
            var abilities = this.Query<AbilityHolderUi>().ToList();
            var ability = abilities.Find(x => x.buttonType == abilityUiData.ButtonType);
            
            ability?.Bind(abilityUiData.UpdateEvent);
        }


        private void Initialize()
        {
            if (initializeAbilityListener != null) 
                return;
            
            initializeAbilityListener = new AbilityUiEventListener(GetUiData<HudUiData>().InitializeAbilityEvent);
            initializeAbilityListener.AddCallback(BindAbilityToUi);
        }
        
        public override void CloseLogic()
        {
            initializeAbilityListener?.RemoveCallbacks();
            initializeAbilityListener = null;

        }
    }
}