using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EtVK.Core.Utyles;
using EtVK.Save_System_Module;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EtVK.Core.Manager
{
    public class GameManager : MonoSingletone<GameManager>, ISavable
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private bool startFullGame;
        public bool IsLoadingScene => isLoadingScene;
        public bool PreventLoad { get; set; }

        public LoadingEvent onFinishLoading;
        public LoadingEvent onLateFinishLoading;
        public ChangeGameState onChangeGameState;

        public delegate void LoadingEvent();
        public delegate void ChangeGameState(bool state);


        private bool gamePaused;
        
        // private Dictionary<SceneNames, Scene> currentLoadedScenes = new();
        private bool isLoadingScene;
        private bool isUnloadingScene;
        private List<SceneNames> currentScenesLoaded = new();
        

        private void Awake()
        {
            InitializeSingletone();
            if (startFullGame)
            {
                InitializeGame();
            }
        }

        public void LoadScene(List<SceneNames> scenesToLoad, bool alter = true)
        {
            if (isLoadingScene)
            {
                Debug.Log("Load scene called while loading a scene");
                return;
            }

            isLoadingScene = true;
            Time.timeScale = 1f;
            StartCoroutine(LoadSceneAsync(scenesToLoad));

            if (!alter)
                return;
            scenesToLoad.ForEach(x => currentScenesLoaded.Add(x));
        }

        public void LoadScene(SceneNames sceneToLoad, bool alter = true)
        {
            if (isLoadingScene)
            {
                Debug.Log("Load scene called while loading a scene");
                return;
            }

            isLoadingScene = true;
            Time.timeScale = 1f;
            StartCoroutine(LoadSceneAsync(new List<SceneNames> {sceneToLoad}));

            if (!alter)
                return;
            currentScenesLoaded.Add(sceneToLoad);
        }

        public void UnLoadScene(List<SceneNames> scenesToUnload, bool alter = true)
        {
            if (isUnloadingScene)
            {
                Debug.Log("Load scene called while unloading a scene");
                return;
            }

            isUnloadingScene = true;
            StartCoroutine(UnloadSceneAsync(scenesToUnload));

            if (!alter)
                return;
            scenesToUnload.ForEach(x => currentScenesLoaded.Remove(x));
        }

        public void UnLoadScene(SceneNames sceneToUnload, bool alter = true)
        {
            if (isUnloadingScene)
            {
                Debug.Log("Load scene called while unloading a scene");
                return;
            }

            isUnloadingScene = true;
            StartCoroutine(UnloadSceneAsync(new List<SceneNames> {sceneToUnload}));

            if (!alter)
                return;
            currentScenesLoaded.Remove(sceneToUnload);
        }

        public void LoadCurrentScenes()
        {
            LoadScene(currentScenesLoaded, false);
        }

        public void UnloadCurrentScenes()
        {
            UnLoadScene(currentScenesLoaded, false);
        }

        public object SaveState()
        {
            return new SaveData(currentScenesLoaded);
        }

        public void LoadState(object state)
        {
            if (PreventLoad)
                return;

            var saveData = (SaveData) state;
            currentScenesLoaded = saveData.SceneNames;
        }

        public void SetFullGameState(bool state)
        {
            startFullGame = state;
            FindObjectsOfType<FullGameObjectTool>().ToList().ForEach(x => x.SetFullGameState(state));
        }

        private IEnumerator LoadSceneAsync(List<SceneNames> scenesToLoad)
        {
            while (isUnloadingScene)
            {
                yield return null;
            }

            StartLoadingScreen();
            var operations = new List<AsyncOperation>();

            foreach (var sceneName in scenesToLoad)
            {
                var operation = SceneManager.LoadSceneAsync(sceneName + " Scene", LoadSceneMode.Additive);
                operations.Add(operation);
            }


            while (operations.Find(x => !x.isDone) != null)
            {
                Debug.Log("Loading");
                yield return null;
            }


            isLoadingScene = false;
            FinishLoading();
        }

        private IEnumerator UnloadSceneAsync(List<SceneNames> scenesToUnload)
        {
            while (isLoadingScene)
            {
                yield return null;
            }

            StartLoadingScreen();
            var operations = new List<AsyncOperation>();

            foreach (var sceneName in scenesToUnload)
            {
                var operation = SceneManager.UnloadSceneAsync(sceneName + " Scene");
                operations.Add(operation);
            }


            while (operations.Find(x => !x.isDone) != null)
            {
                Debug.Log("Unloading");
                yield return null;
            }

            isUnloadingScene = false;
            FinishLoading();
        }

        private void FinishLoading()
        {
            if (isLoadingScene || isUnloadingScene)
                return;

            IsGamePaused = false;
            onFinishLoading?.Invoke();
            StartCoroutine(LoadingDelay(2f));
        }

        private void StartLoadingScreen()
        {
            if (loadingScreen.activeInHierarchy)
                return;


            loadingScreen.SetActive(true);
        }

        private IEnumerator LoadingDelay(float time)
        {
            yield return new WaitForSecondsRealtime(time/2f);
            onLateFinishLoading?.Invoke();
            
            yield return new WaitForSecondsRealtime(time/2f);
            loadingScreen.SetActive(false);
        }

        private void InitializeGame()
        {
            var scene = new List<SceneNames> {SceneNames.MainMenu};
            LoadScene(scene);
        }
        
        public bool IsGamePaused
        {
            get => gamePaused;
            set
            {
                gamePaused = value;
                onChangeGameState?.Invoke(value);
            }
        }

        [System.Serializable]
        private struct SaveData
        {
            public List<SceneNames> SceneNames { get; set; }

            public SaveData(List<SceneNames> sceneNames)
            {
                SceneNames = sceneNames;
            }
        }
    }
}