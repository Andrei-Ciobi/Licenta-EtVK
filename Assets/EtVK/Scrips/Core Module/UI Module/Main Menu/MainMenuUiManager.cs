using EtVK.Core;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.UI_Module.Main_Menu
{
    public class MainMenuUiManager : MonoBehaviour
    {

        public void StartNewGame()
        {
            GameManager.Instance.UnLoadScene(SceneNames.Menu);
            GameManager.Instance.LoadScene(SceneNames.CharacterCreation);
        }
    }
}