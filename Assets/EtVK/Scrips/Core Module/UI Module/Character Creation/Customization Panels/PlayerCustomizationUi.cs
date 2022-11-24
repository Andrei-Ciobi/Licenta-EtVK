using System.Collections.Generic;
using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Save_System_Module;
using EtVK.UI_Module.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Character_Creation.Customization_Panels
{
    public class PlayerCustomizationUi : BasePanel<CharacterCreationManager>
    {
        private Button currentButton;
        private BasePanel<CharacterCreationManager> currentPanelActive;

        private readonly string leftSidePanel = "left-side-panel";
        private readonly string bottomBar = "bottom-bar";


        public new class UxmlFactory : UxmlFactory<PlayerCustomizationUi, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            var hairButton = this.Q(leftSidePanel)?.Q<Button>("hair");
            var facialHairButton = this.Q(leftSidePanel)?.Q<Button>("facial-hair");
            var eyebrowsButton = this.Q(leftSidePanel)?.Q<Button>("eyebrows");
            var facialMarkButton = this.Q(leftSidePanel)?.Q<Button>("facial-mark");
            var colorsButton = this.Q(leftSidePanel)?.Q<Button>("colors");

            var backButton = this.Q(bottomBar).Q<Button>("back");
            var nextButton = this.Q(bottomBar).Q<Button>("next");

            var hairSidePanel = this.Q<BasePanel<CharacterCreationManager>>("hair-side-panel");
            var facialHairSidePanel = this.Q<BasePanel<CharacterCreationManager>>("facial-hair-side-panel");
            var eyebrowsSidePanel = this.Q<BasePanel<CharacterCreationManager>>("eyebrows-side-panel");
            var facialMarkSidePanel = this.Q<BasePanel<CharacterCreationManager>>("facial-mark-side-panel");
            var colorsSidePanel = this.Q<BasePanel<CharacterCreationManager>>("colors-side-panel");

            // Click events
            hairButton?.RegisterCallback<ClickEvent>(ev =>
                PlayClickButtonSound(() => OpenSidePanel(hairSidePanel, hairButton)));
            facialHairButton?.RegisterCallback<ClickEvent>(ev =>
                PlayClickButtonSound(() => OpenSidePanel(facialHairSidePanel, facialHairButton)));
            eyebrowsButton?.RegisterCallback<ClickEvent>(ev =>
                PlayClickButtonSound(() => OpenSidePanel(eyebrowsSidePanel, eyebrowsButton)));
            facialMarkButton?.RegisterCallback<ClickEvent>(ev =>
                PlayClickButtonSound(() => OpenSidePanel(facialMarkSidePanel, facialMarkButton)));
            colorsButton?.RegisterCallback<ClickEvent>(ev =>
                PlayClickButtonSound(() => OpenSidePanel(colorsSidePanel, colorsButton)));
            backButton?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(OnBack));
            nextButton?.RegisterCallback<ClickEvent>(ev => PlayClickButtonSound(OnNext));
            
            // Hover events
            hairButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            facialHairButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            eyebrowsButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            facialMarkButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            colorsButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            backButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());
            nextButton?.RegisterCallback<MouseOverEvent>(ev => PlayHoverButtonSound());

            base.OnGeometryChange(evt);
        }

        private void OpenSidePanel(BasePanel<CharacterCreationManager> sidePanel, Button buttonClicked)
        {
            if (currentPanelActive == sidePanel)
                return;

            if (currentButton != null)
            {
                currentButton.AddToClassList("button-menu");
                currentButton.RemoveFromClassList("button-menu-active");
            }

            if (buttonClicked != null)
            {
                buttonClicked.RemoveFromClassList("button-menu");
                buttonClicked.AddToClassList("button-menu-active");
                currentButton = buttonClicked;
            }

            BaseUiManager.OpenPanelStart(currentPanelActive, sidePanel);
            currentPanelActive = sidePanel;
        }

        private void OnBack()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            GameManager.Instance.StartLoadingScreen(GameUi.MainMenu);
            GameManager.Instance.UnLoadScene(SceneNames.CharacterCreation);
        }

        private void OnNext()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            GameManager.Instance.StartLoadingScreen(GameUi.Hud);
            var scenesToLoad = new List<SceneNames>
            {
                SceneNames.LevelOne,
                SceneNames.Player,
            };

            GameSaveManager.Instance.Save();
            GameManager.Instance.UnLoadScene(SceneNames.CharacterCreation);
            GameManager.Instance.LoadScene(scenesToLoad);
        }
    }
}