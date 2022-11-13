using EtVK.UI_Module.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu.Panels
{
    public class MainMenuUi : BaseMenuPanel
    {
        private VisualElement titleContainer;
        
        private Button startGameButton;
        private Button loadGameButton;
        private Button optionsButton;
        private Button exitButton;
        private Button enterButton;

        public new class UxmlFactory : UxmlFactory<MainMenuUi, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        protected override void OnGeometryChange(GeometryChangedEvent evt)
        {
            
            titleContainer = this.Q<VisualElement>("title-container");
            
            enterButton = this.Q<Button>("enter-menu");
            startGameButton = this.Q<Button>("start-game");
            loadGameButton = this.Q<Button>("load-game");
            optionsButton = this.Q<Button>("options");
            exitButton = this.Q<Button>("exit");

            startGameButton?.RegisterCallback<ClickEvent>(ev =>
                MainMenuManager.OpenPanelStart(this, MainMenuManager.StartMenu));
            
            enterButton?.RegisterCallback<ClickEvent>(ev => TransitionEnterScreen());

            //Transition
            enterButton?.RegisterCallback<TransitionEndEvent>(DisplayMainMenuFromStart);
            RegisterCallback<TransitionEndEvent>(ev => MainMenuManager.ClosePanelEnd(ev, this));
            RegisterCallback<TransitionEndEvent>(ev => MainMenuManager.OpenPanelEnd(ev, this));
            
            base.OnGeometryChange(evt);
        }
        
        

        private void TransitionEnterScreen()
        {
            enterButton?.AddToClassList("opacity-none-trans");
            titleContainer?.AddToClassList("opacity-none-trans");
        }


        private void DisplayMainMenuFromStart(TransitionEndEvent evt)
        {
            if(!evt.stylePropertyNames.Contains("opacity"))
                return;
            
            enterButton?.AddToClassList("opacity-none");
            enterButton?.RemoveFromClassList("opacity-none-trans");
            if (enterButton != null) 
                enterButton.style.display = DisplayStyle.None;
            
            titleContainer?.AddToClassList("opacity-none");
            titleContainer?.RemoveFromClassList("opacity-none-trans");
            
            DisplayContent();

        }

        private void DisplayContent()
        {
            Display(startGameButton);
            Display(loadGameButton);
            Display(optionsButton);
            Display(exitButton);
            Display(titleContainer);
        }


        private void Display([CanBeNull] Button button)
        {
            if (button != null)
            {
                button.style.display = DisplayStyle.Flex;
            }
            
            button?.AddToClassList("opacity-full-trans");
        }
        
        private void Display([CanBeNull] VisualElement element)
        {
            if (element != null)
            {
                element.style.display = DisplayStyle.Flex;
            }
            
            element?.AddToClassList("opacity-full-trans");
        }
    }
}