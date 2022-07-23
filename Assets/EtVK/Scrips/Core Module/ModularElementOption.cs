using System.Collections.Generic;
using System.Linq;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Core_Module
{
    public class ModularElementOption : MonoBehaviour
    {
        [SerializeField] private ModularOptions type;
        private List<GameObject> elementOptions = new List<GameObject>();
        private List<SkinnedMeshRenderer> renderers = new List<SkinnedMeshRenderer>();
        private int currentIndex = -1;

        public ModularOptions Type => type;
        public Material Mat { get; set; }

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

                if (child.activeInHierarchy)
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
            
            // for (int i = 0; i < transform.childCount; i++)
            // {
            //     var child = transform.GetChild(i).gameObject;
            //
            //     if (child.activeInHierarchy)
            //     {
            //         if (currentIndex == 0)
            //         {
            //             currentIndex = i;
            //         }
            //         else
            //         {
            //             child.SetActive(false);
            //         }
            //     }
            //     
            //     elementOptions.Add(child);
            // }
        }

        public IEnumerable<SkinnedMeshRenderer> GetMeshRenderers()
        {
            return renderers;
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
        
        
    }
}