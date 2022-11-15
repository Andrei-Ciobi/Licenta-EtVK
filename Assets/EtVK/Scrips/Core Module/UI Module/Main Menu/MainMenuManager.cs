using EtVK.UI_Module.Main_Menu.Panels;
using UnityEngine.UIElements;

namespace EtVK.UI_Module.Main_Menu
{
    public class MainMenuManager : VisualElement
    {
        public MainMenuUi MainMenu => mainMenu;
        public StartMenuUi StartMenu => startMenu;
        public LoadMenuUi LoadMenu => loadMenu;

        private MainMenuUi mainMenu;
        private StartMenuUi startMenu;
        private LoadMenuUi loadMenu;
        // private EnterMenuUi enterMenu;

        private static BaseMenuPanel selectedPanel;

        public new class UxmlFactory : UxmlFactory<MainMenuManager, UxmlTraits>
        {
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        public MainMenuManager()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        private void OnGeometryChange(GeometryChangedEvent evt)
        {
            mainMenu = this.Q<MainMenuUi>("main-menu");
            startMenu = this.Q<StartMenuUi>("start-menu");
            loadMenu = this.Q<LoadMenuUi>("load-menu");
            // enterMenu = this.Q<EnterMenuUi>("enter-menu");
            
            // mainMenu?.RegisterCallback<TransitionEndEvent>(ev => ClosePanelEnd(ev, mainMenu));
            // startMenu?.RegisterCallback<TransitionEndEvent>(ev => ClosePanelEnd(ev, startMenu));
            //
            // mainMenu?.RegisterCallback<TransitionEndEvent>(ev => OpenPanelEnd(ev, mainMenu));
            // startMenu?.RegisterCallback<TransitionEndEvent>(ev => OpenPanelEnd(ev, startMenu));

            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        public void OpenPanelStart(BaseMenuPanel from, BaseMenuPanel to)
        {
            from?.Close();
            selectedPanel = to;
        }

        public void OpenPanelEnd(TransitionEndEvent evt, BaseMenuPanel basePanel)
        {
            if(selectedPanel == null)
                return;
            
            if (!evt.stylePropertyNames.Contains("opacity"))
                return;
            
            if (!basePanel.ClassListContains("opacity-full-trans"))
                return;
            
            basePanel.OpenEnd();
            
            selectedPanel = null;
        }

        public void ClosePanelEnd(TransitionEndEvent evt, BaseMenuPanel basePanel)
        {
            if(selectedPanel == null)
                return;
            
            if (!evt.stylePropertyNames.Contains("opacity"))
                return;
            
            if (!basePanel.ClassListContains("opacity-none-trans"))
                return;
            
            basePanel.CloseEnd();
            selectedPanel.Open();
        }

        // [SerializeField] private GameObject saveFilePrefab;
        // [SerializeField] private RectTransform saveFileContainer;
        // [SerializeField] private List<GameObject> sidePanels;
        //
        // private void Awake()
        // {
        //     InitializeSaveFiles();
        // }
        //
        // public void StartNewGame()
        // {
        //     GameManager.Instance.UnLoadScene(SceneNames.MainMenu);
        //     GameManager.Instance.LoadScene(SceneNames.CharacterCreation);
        // }
        //
        //
        // public void UpdateSaveFileName(string fileName)
        // {
        //     GameSaveManager.Instance.SetSaveFileName(fileName);
        // }
        //
        // public void ChangeSideMenu(GameObject sideMenu)
        // {
        //     sidePanels.ForEach(x => x.SetActive(false));
        //     sideMenu.SetActive(true);
        // }
        //
        // private void InitializeSaveFiles()
        // {
        //     var saveFiles = GameSaveManager.Instance.GetAllSaves();
        //
        //     if (saveFiles == null)
        //         return;
        //
        //     foreach (var saveFile in saveFiles)
        //     {
        //         var prefab = Instantiate(saveFilePrefab, saveFileContainer)
        //             .GetComponentInChildren<TextMeshProUGUI>();
        //         prefab.text = saveFile;
        //         var newRect = saveFileContainer.sizeDelta;
        //         newRect.y += 230f;
        //         saveFileContainer.sizeDelta = newRect;
        //     }
        // }
    }
}