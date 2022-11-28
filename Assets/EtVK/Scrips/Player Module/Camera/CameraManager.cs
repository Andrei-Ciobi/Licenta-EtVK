using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using EtVK.Core.Manager;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using UnityEngine;

namespace EtVK.Player_Module.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private List<SerializableSet<ActiveCameraType, CinemachineVirtualCamera>> cameraList = new();

        private CinemachineVirtualCamera currentCamera;
        private List<CinemachineInputProvider> cameraInputList;

        private void Awake()
        {
            cameraInputList = GetComponentsInChildren<CinemachineInputProvider>(true).ToList();
        }

        private void Start()
        {
            OnStart();
        }


        public void ChangeCamera(ActiveCamera cam)
        {
            var newCamera = cameraList.Find(x => x.GetKey().Equals(cam.CameraType)).GetValue();

            if (newCamera == null)
            {
                Debug.LogError($"No camera type found {cam.CameraType}");
                return;
            }

            newCamera.gameObject.SetActive(true);
            currentCamera.gameObject.SetActive(false);

            currentCamera = newCamera;
            currentCamera.LookAt = cam.CameraType.Equals(ActiveCameraType.LockOn) ? cam.TargetTransform : null;
        }

        public void ChangeCameraInputState(bool state)
        {
            switch (state)
            {
                case true:
                    cameraInputList.ForEach(x => x.XYAxis.action.Enable());
                    break;
                case false:
                    cameraInputList.ForEach(x => x.XYAxis.action.Disable());
                    break;
            }
        }


        private void OnStart()
        {
            foreach (var set in cameraList)
            {
                set.GetValue().gameObject.SetActive(false);
            }

            currentCamera = cameraList.Find(x => x.GetKey() == ActiveCameraType.Main).GetValue();
            currentCamera.gameObject.SetActive(true);

            if(GameManager.Instance.IsFullGame)
                return;
            
            cameraInputList.ForEach(x => x.XYAxis.action.Enable());
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
    }
}