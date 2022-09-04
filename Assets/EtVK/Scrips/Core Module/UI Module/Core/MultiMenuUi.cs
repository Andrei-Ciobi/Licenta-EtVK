using System.Collections.Generic;
using EtVK.UI_Module.Core.Interfaces;
using UnityEngine;

namespace EtVK.UI_Module.Core
{
    public abstract class MultiMenuUi : MonoBehaviour, IMultiMenuUi
    {
        
        [SerializeField] private List<GameObject> menuList;
        
        private int currentMenuIndex;
        public void NextMenu()
        {
            if (currentMenuIndex + 1 >= menuList.Count)
            {
                OnMaxMenuIndex();
                return;
            }

            menuList[currentMenuIndex].SetActive(false);
            currentMenuIndex++;
            menuList[currentMenuIndex].SetActive(true);
        }

        public void PreviousMenu()
        {
            if (currentMenuIndex - 1 < 0)
            {
                OnMinMenuIndex();
                return;
            }
            
            menuList[currentMenuIndex].SetActive(false);
            currentMenuIndex--;
            menuList[currentMenuIndex].SetActive(true);
        }

        protected abstract void OnMaxMenuIndex();
        protected abstract void OnMinMenuIndex();
    }
}