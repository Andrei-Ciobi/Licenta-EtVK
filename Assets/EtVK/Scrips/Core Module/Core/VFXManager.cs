using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace EtVK.Core
{
    public class VFXManager : MonoSingletone<VFXManager>
    {
        private void Awake()
        {
            InitializeSingletone();
        }

        public void PlayPostProcessing(GameObject postProcessingObj, float time, AnimationCurve timeCurve)
        {
            var postObj = Instantiate(postProcessingObj);
            postObj.SetActive(true);
            var postProcessing = postObj.GetComponent<Volume>();
            if (postProcessing == null)
                return;

            StartCoroutine(PostProcessingCoroutine(postProcessing, time, timeCurve));
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