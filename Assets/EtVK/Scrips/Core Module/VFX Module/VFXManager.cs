using System.Collections;
using EtVK.Core;
using UnityEngine;
using UnityEngine.Rendering;

namespace EtVK.VFX_Module
{
    public class VFXManager : MonoSingletone<VFXManager>
    {
        private void Awake()
        {
            InitializeSingletone();
        }

        public void PlayPostProcessing(VolumeVFX volumeVFX, float time, bool intoWorld = true)
        {
            if (volumeVFX == null)
            {
                Debug.LogError("No volume vfx given!");
                return;
            }
            
            Transform spawnPoint = null;

            if (intoWorld)
            {
                spawnPoint = GameObject.FindGameObjectWithTag("PostProcessing")?.transform;
            }
            
            var postObj = Instantiate(volumeVFX.Obj, spawnPoint);
            postObj.SetActive(true);
            var postProcessing = postObj.GetComponent<Volume>();
            if (postProcessing == null)
                return;

            StartCoroutine(PostProcessingCoroutine(postProcessing, time, volumeVFX.TimeCurve));
        }


        private IEnumerator PostProcessingCoroutine(Volume postProcessing, float time, AnimationCurve timeCurve)
        {
            var currentTime = 0f;

            while (time > currentTime)
            {
                var percentage = currentTime / time;
                var weight = timeCurve.Evaluate(percentage);

                postProcessing.weight = weight;
                currentTime += Time.deltaTime;

                yield return null;
            }

            Destroy(postProcessing.gameObject);
        }
    }
}