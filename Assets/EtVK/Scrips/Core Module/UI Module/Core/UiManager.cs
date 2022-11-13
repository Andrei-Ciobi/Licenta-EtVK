using System;
using EtVK.UI_Module.Main_Menu;
using EtVK.UI_Module.Main_Menu.Panels;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Core
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;

        public UIDocument UIDocument => uiDocument;

        private void Start()
        {
            // OnStart();
        }


        public MainMenuManager GetMainMenuManager()
        {
            return uiDocument.rootVisualElement.Q<MainMenuManager>("main-menu-manager");
        }
        
    }
}