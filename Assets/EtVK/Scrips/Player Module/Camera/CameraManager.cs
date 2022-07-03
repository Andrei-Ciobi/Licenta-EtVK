using EtVK.Scrips.Core_Module;
using UnityEngine;

namespace EtVK.Scrips.Player_Module.Camera {
    public class CameraManager : MonoSingletone<CameraManager>
    {

        [SerializeField]
        private GameObject aimCamera;
        [SerializeField]
        private GameObject mainCamera;

        public static bool aimCameraActive = false;

        private void Awake()
        {
            InitializeSingletone();

        }


        // Update is called once per frame
        // void Update()
        // {
        //
        //     if (UIManager.isGamedPaused)
        //     {
        //         mainCamera.SetActive(false);
        //     }
        //     else if (!mainCamera.activeSelf)
        //     {
        //         mainCamera.SetActive(true);
        //     }
        // }

        public void SetActiveAimCamera(bool value)
        {
            aimCameraActive = value;
            aimCamera.SetActive(value);
        }
    }
}