using EtVK.Core;
using EtVK.Save_System_Module;
using UnityEngine;

namespace EtVK.Levels_Module
{
    public class BaseLevelManager : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;

        private void Awake()
        {
            GameManager.Instance.onFinishLoading += LoadCurrentLevelData;
            GameManager.Instance.onFinishLoading += SetPlayerPosition;
        }

        private void LoadCurrentLevelData()
        {
            GameSaveManager.Instance.Load();
        }

        [ContextMenu("Set Player Position")]
        private void SetPlayerPosition()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.Log("No Player found in the game");
            }
            else
            {
                player.transform.position = playerSpawnPoint.transform.position;
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.onFinishLoading -= LoadCurrentLevelData;
            GameManager.Instance.onFinishLoading -= SetPlayerPosition;
        }
    }
}