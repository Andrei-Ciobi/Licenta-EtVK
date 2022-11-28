using EtVK.Event_Module.Events;
using UnityEngine;

namespace EtVK.UI_Module.Core
{
    [CreateAssetMenu(menuName = "ScriptableObjects/UiData/Audio")]
    public class AudioUiData : BaseUiData
    {
        [SerializeField] private AudioEvent playAudioEvent;
        [SerializeField] private AudioClip buttonHoverSound;
        [SerializeField] private AudioClip buttonClickSound;

        public AudioEvent PlayAudioEvent => playAudioEvent;
        public AudioClip ButtonHoverSound => buttonHoverSound;
        public AudioClip ButtonClickSound => buttonClickSound;
    }
}