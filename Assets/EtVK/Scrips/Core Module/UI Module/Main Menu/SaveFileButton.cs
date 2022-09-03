using EtVK.Core;
using EtVK.Save_System_Module;
using EtVK.Utyles;
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
            GameManager.Instance.UnLoadScene(SceneNames.Menu);
            GameManager.Instance.LoadCurrentScenes();
        }
    }
}