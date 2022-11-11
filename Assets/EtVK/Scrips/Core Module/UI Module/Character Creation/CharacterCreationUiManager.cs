using System.Collections.Generic;
using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Input_Module;
using EtVK.Save_System_Module;
using EtVK.UI_Module.Core;

namespace EtVK.UI_Module.Character_Creation
{
    public class CharacterCreationUiManager : MultiMenuUi
    {
        private void Start()
        {
            InputManager.Instance.DisablePlayerActionMap();
            InputManager.Instance.EnableUIActionMap();
            
        }
        
        protected override void OnMaxMenuIndex()
        {
            var scenesToLoad = new List<SceneNames>
            {
                SceneNames.LevelOne,
                SceneNames.Player,
                SceneNames.GameUi,
            };
            
            GameSaveManager.Instance.Save();
            GameManager.Instance.UnLoadScene(SceneNames.CharacterCreation);
            GameManager.Instance.LoadScene(scenesToLoad);
        }

        protected override void OnMinMenuIndex()
        {
            GameManager.Instance.UnLoadScene(SceneNames.CharacterCreation);
            GameManager.Instance.LoadScene(SceneNames.MainMenu);
        }
        
    }
}