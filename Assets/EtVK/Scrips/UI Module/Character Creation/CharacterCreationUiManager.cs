﻿using System;
using EtVK.Input_Module;
using UnityEngine;
using UnityEngine.UI;

namespace EtVK.UI_Module.Character_Creation
{
    public class CharacterCreationUiManager : MonoBehaviour
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
    }
}