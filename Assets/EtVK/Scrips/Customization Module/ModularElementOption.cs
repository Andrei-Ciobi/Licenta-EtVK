using System.Collections.Generic;
using System.Linq;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Customization_Module
{
    public class ModularElementOption : MonoBehaviour
    {
        [SerializeField] private ModularOptions type;
        [SerializeField] private bool partialInitialize;
        public ModularOptions Type => type;
        public Material Mat { get; set; }

        private List<GameObject> elementOptions = new();
        private List<SkinnedMeshRenderer> renderers = new();
        private int currentIndex = -1;


        public void Initialize()
        {
            currentIndex = -1;
            renderers = new List<SkinnedMeshRenderer>();
            elementOptions = new List<GameObject>();

            renderers = GetComponentsInChildren<SkinnedMeshRenderer>(true).ToList();
            Mat = renderers[0].sharedMaterial;

            foreach (var obj in renderers)
            {
                var child = obj.gameObject;
                
                
                if (child.activeInHierarchy && !partialInitialize)
                {
                    if (currentIndex == -1)
                    {
                        currentIndex = renderers.IndexOf(obj);
                    }
                    else
                    {
                        child.SetActive(false);
                    }
                }
                
                elementOptions.Add(child);
            }
        }

        public SkinnedMeshRenderer GetCurrentElement()
        {
            return currentIndex == -1 ? null : renderers[currentIndex];
        }

        public void Next()
        {
            if (currentIndex == -1)
            {
                currentIndex = 0;
            }
            else
            {
                elementOptions[currentIndex].SetActive(false);
                currentIndex++;
            }
            
            if (currentIndex == elementOptions.Count)
            {
                currentIndex = 0;
            }
            elementOptions[currentIndex].SetActive(true);
        }
        
        public void Previous()
        {
            if (currentIndex == -1)
            {
                currentIndex = elementOptions.Count - 1;
            }
            else
            {
                elementOptions[currentIndex].SetActive(false);
                currentIndex--;
            }
            
            if (currentIndex == -1)
            {
                currentIndex = elementOptions.Count - 1;
            }
            elementOptions[currentIndex].SetActive(true);
        }

        public void SetDefault()
        {
            if (currentIndex != -1)
            {
                elementOptions[currentIndex].SetActive(false);
            }

            currentIndex = 0;
            elementOptions[currentIndex].SetActive(true);
        }

        public void Unset()
        {
            if (currentIndex != -1)
            {
                elementOptions[currentIndex].SetActive(false);
            }

            currentIndex = -1;
        }

        public void SetMaterial(Material newMaterial)
        {
            renderers.ForEach(x=> x.material = newMaterial);
            Mat = newMaterial;
        }

        public void DestroyInactive()
        {
            if (currentIndex == -1)
            {
                elementOptions[0].SetActive(true);
            }
            
            foreach (var obj in elementOptions)
            {
                if (!obj.activeInHierarchy)
                {
                    Debug.Log(obj.name);
                    DestroyImmediate(obj.gameObject);
                }
            }

            if (currentIndex == -1)
            {
                elementOptions[0].SetActive(false);
            }
        }
        
        
    }
}