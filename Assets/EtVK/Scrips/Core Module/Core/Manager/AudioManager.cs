using System.Collections.Generic;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using UnityEngine;

namespace EtVK.Core.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private List<SerializableSet<AudioSourceType, AudioSource>> audioSourceList;

        public void PlaySound(AudioData data)
        {
            var audioSource = audioSourceList.Find(x => x.GetKey() == data.AudioSourceType)?.GetValue();

            if (audioSource == null)
            {
                Debug.LogError($"No audio source for {data.AudioSourceType} type");
                return;
            }

            audioSource.clip = data.AudioClip;
            audioSource.Play();
        }
    }
}