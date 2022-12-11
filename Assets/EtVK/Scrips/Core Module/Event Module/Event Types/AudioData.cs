using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Event_Module.Event_Types
{
    public struct AudioData
    {
        public AudioClip AudioClip { get; set; }
        public AudioSourceType AudioSourceType { get; set; }

        public AudioData(AudioSourceType audioSourceType, AudioClip audioClip)
        {
            AudioClip = audioClip;
            AudioSourceType = audioSourceType;
        }
    }
}