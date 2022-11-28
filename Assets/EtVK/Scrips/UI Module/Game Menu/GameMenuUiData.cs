using EtVK.Event_Module.Events;
using EtVK.UI_Module.Core;
using UnityEngine;

namespace EtVK.UI_Module.Game_Menu
{
    [CreateAssetMenu( menuName = "ScriptableObjects/UiData/GameMenu")]
    public class GameMenuUiData : BaseUiData
    {
        [SerializeField] private BoolEvent cameraInputEvent;

        public BoolEvent CameraInputEvent => cameraInputEvent;
    }
}