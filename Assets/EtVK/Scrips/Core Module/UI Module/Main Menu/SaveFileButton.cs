using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Save_System_Module;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EtVK.UI_Module.Main_Menu
{
    public class SaveFileButton : MonoBehaviour
    {
        private TextMeshProUGUI saveName;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
            saveName = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnClick()
        {
            GameSaveManager.Instance.LoadSaveFileWithName(saveName.text);
            GameManager.Instance.UnLoadScene(SceneNames.MainMenu);
            GameManager.Instance.LoadCurrentScenes();
        }
    }
}