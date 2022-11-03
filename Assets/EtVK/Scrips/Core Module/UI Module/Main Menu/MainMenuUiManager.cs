using System.Collections.Generic;
using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Save_System_Module;
using TMPro;
using UnityEngine;

namespace EtVK.UI_Module.Main_Menu
{
    public class MainMenuUiManager : MonoBehaviour
    {
        [SerializeField] private GameObject saveFilePrefab;
        [SerializeField] private RectTransform saveFileContainer;
        [SerializeField] private List<GameObject> sidePanels;

        private void Awake()
        {
            InitializeSaveFiles();
        }

        public void StartNewGame()
        {
            GameManager.Instance.UnLoadScene(SceneNames.MainMenu);
            GameManager.Instance.LoadScene(SceneNames.CharacterCreation);
        }


        public void UpdateSaveFileName(string fileName)
        {
            GameSaveManager.Instance.SetSaveFileName(fileName);
        }

        public void ChangeSideMenu(GameObject sideMenu)
        {
            sidePanels.ForEach(x => x.SetActive(false));
            sideMenu.SetActive(true);
        }

        private void InitializeSaveFiles()
        {
            var saveFiles = GameSaveManager.Instance.GetAllSaves();

            if (saveFiles == null)
                return;

            foreach (var saveFile in saveFiles)
            {
                var prefab = Instantiate(saveFilePrefab, saveFileContainer)
                    .GetComponentInChildren<TextMeshProUGUI>();
                prefab.text = saveFile;
                var newRect = saveFileContainer.sizeDelta;
                newRect.y += 230f;
                saveFileContainer.sizeDelta = newRect;
            }
        }
    }
}