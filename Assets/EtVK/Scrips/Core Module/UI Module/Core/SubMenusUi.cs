using EtVK.UI_Module.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace EtVK.UI_Module.Core
{
    public class SubMenusUi : MonoBehaviour, ISubMenusUi
    {
        [Header("Default button color")]
        [SerializeField] private ColorBlock defaultButtonColor = ColorBlock.defaultColorBlock;
        [Header("Active button color")]
        [SerializeField] private ColorBlock activeButtonColor = ColorBlock.defaultColorBlock;
        
        private Button currentButtonActive;
        private GameObject currentSubmenuActive;
        
        public void ChangeSubMenu(GameObject submenu)
        {
            if (currentSubmenuActive != null)
            {
                currentSubmenuActive.SetActive(false);
            }

            currentSubmenuActive = submenu;
            currentSubmenuActive.SetActive(true);
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