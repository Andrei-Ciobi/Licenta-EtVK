using System;
using System.Collections.Generic;
using EtVK.Core;
using EtVK.Input_Module;
using EtVK.Save_System_Module;
using EtVK.UI_Module.Core;
using EtVK.Utyles;
using UnityEngine;
using UnityEngine.UI;

namespace EtVK.UI_Module.Character_Creation
{
    public class CharacterCreationUiManager : UiManager
    {
        [Header("Default button color")]
        [SerializeField] private ColorBlock defaultButtonColor;
        [Header("Active button color")]
        [SerializeField] private ColorBlock activeButtonColor;
        private GameObject currentSubMenuActive;
        private Button currentButtonActive;

        private void Start()
        {
            InputManager.Instance.DisablePlayerActionMap();
            InputManager.Instance.EnableUIActionMap();
        }

        public void ChangeSubMenu(GameObject submenu)
        {
            if (currentSubMenuActive != null)
            {
                currentSubMenuActive.SetActive(false);
            }

            currentSubMenuActive = submenu;
            currentSubMenuActive.SetActive(true);
        }

        public void ChangeActiveButton(Button button)
        {
            if (currentButtonActive != null)
            {
                currentButtonActive.colors = defaultButtonColor;
            }

            currentButtonActive = button;
            currentButtonActive.colors = activeButtonColor;
        }

        protected override void OnMaxMenuIndex()
        {
            var scenesToLoad = new List<SceneNames>
            {
                SceneNames.LevelOne,
                SceneNames.Player,
            };
            
            GameSaveManager.Instance.Save();
            GameManager.Instance.UnLoadScene(SceneNames.CharacterCreation);
            GameManager.Instance.LoadScene(scenesToLoad);
        }

        protected override void OnMinMenuIndex()
        {
            GameManager.Instance.UnLoadScene(SceneNames.CharacterCreation);
            GameManager.Instance.LoadScene(SceneNames.Menu);
        }
    }
}