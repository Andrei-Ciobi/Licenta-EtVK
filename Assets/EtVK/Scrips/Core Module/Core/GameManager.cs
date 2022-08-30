using System.Collections;
using System.Collections.Generic;
using EtVK.Utyles;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EtVK.Core
{
    public class GameManager : MonoSingletone<GameManager>
    {
        [SerializeField] private bool startFullGame;
        public bool IsLoadingScene => isLoadingScene;
        
        public LoadingEvent onFinishLoading;
        public delegate void LoadingEvent();
        

        // private Dictionary<SceneNames, Scene> currentLoadedScenes = new();
        private bool isLoadingScene;
        private bool isUnloadingScene;

        private void Awake()
        {
            InitializeSingletone();
            if (startFullGame)
            {
                InitializeGame();
            }
        }

        public void LoadScene(List<SceneNames> scenesToLoad)
        {
            if (isLoadingScene)
            {
                Debug.Log("Load scene called while loading a scene");
                return;
            }

            isLoadingScene = true;
            StartCoroutine(LoadSceneAsync(scenesToLoad));
        }
        
        public void LoadScene(SceneNames sceneToLoad)
        {
            if (isLoadingScene)
            {
                Debug.Log("Load scene called while loading a scene");
                return;
            }

            isLoadingScene = true;
            StartCoroutine(LoadSceneAsync(new List<SceneNames> {sceneToLoad}));
        }

        public void UnLoadScene(List<SceneNames> scenesToUnload)
        {
            if (isUnloadingScene)
            {
                Debug.Log("Load scene called while unloading a scene");
                return;
            }

            isUnloadingScene = true;
            StartCoroutine(UnloadSceneAsync(scenesToUnload));
        }
        
        public void UnLoadScene(SceneNames sceneToUnload)
        {
            if (isUnloadingScene)
            {
                Debug.Log("Load scene called while unloading a scene");
                return;
            }

            isUnloadingScene = true;
            StartCoroutine(UnloadSceneAsync(new List<SceneNames>{sceneToUnload}));
        }

        private IEnumerator LoadSceneAsync(List<SceneNames> scenesToLoad)
        {
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
            FinishLoadingScreen();
        }
        
        private IEnumerator UnloadSceneAsync(List<SceneNames> scenesToUnload)
        {
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
            FinishLoadingScreen();
        }

        private void FinishLoadingScreen()
        {
            if(isLoadingScene || isUnloadingScene)
                return;
            
            onFinishLoading?.Invoke();
        }

        private void InitializeGame()
        {
            var scene = new List<SceneNames> {SceneNames.Menu};
            LoadScene(scene);
        }
    }
}