using System.Collections;
using System.Collections.Generic;
using EtVK.Save_System_Module;
using EtVK.Utyles;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EtVK.Core
{
    public class GameManager : MonoSingletone<GameManager>, ISavable
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private bool startFullGame;
        public bool IsLoadingScene => isLoadingScene;
        public bool PreventLoad { get; set; }

        public LoadingEvent onFinishLoading;

        public delegate void LoadingEvent();


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
            StartCoroutine(LoadSceneAsync(scenesToLoad));
            
            if(!alter)
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
            StartCoroutine(LoadSceneAsync(new List<SceneNames> {sceneToLoad}));
            
            if(!alter)
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
            
            if(!alter)
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
            
            if(!alter)
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
            if(PreventLoad)
                return;
            
            var saveData = (SaveData) state;
            currentScenesLoaded = saveData.SceneNames;
        }

        private IEnumerator LoadSceneAsync(List<SceneNames> scenesToLoad)
        {
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

            onFinishLoading?.Invoke();
            loadingScreen.SetActive(false);
        }

        private void StartLoadingScreen()
        {
            if (loadingScreen.activeInHierarchy)
                return;


            loadingScreen.SetActive(true);
        }

        private void InitializeGame()
        {
            var scene = new List<SceneNames> {SceneNames.Menu};
            LoadScene(scene);
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