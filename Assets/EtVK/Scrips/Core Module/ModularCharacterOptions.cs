using System;
using System.Collections.Generic;
using System.Linq;
using EtVK.Utyles;
using Unity.Collections;
using UnityEngine;

namespace EtVK.Core_Module
{
    public class ModularCharacterOptions : MonoBehaviour
    {
        [Header("Selected material to operate with, on type")]
        [SerializeField] private Material material;
        [SerializeField] private ModularOptions bodyType;
        [Header("The current material and color")]
        [SerializeField][ReadOnly] private Material currentMaterial;
        [SerializeField] private ModularColorOptions colorOption = ModularColorOptions.Primary;
        [SerializeField] private Color color;
        
        private List<ModularElementOption> elements = new();
        private ModularColorOptions lastColorOption = ModularColorOptions.Primary;
        private ModularOptions lastCurrentType = ModularOptions.Head;

        private void OnValidate()
        {
            if (bodyType != lastCurrentType)
            {
                currentMaterial = elements.Find(x => x.Type.Equals(bodyType)).Mat;
                color = currentMaterial.GetColor($"_Color_{colorOption.ToString()}");
                lastCurrentType = bodyType;
            }
            else if (colorOption != lastColorOption)
            {
                currentMaterial = elements.Find(x => x.Type.Equals(bodyType)).Mat;
                color = currentMaterial.GetColor($"_Color_{colorOption.ToString()}");
                lastColorOption = colorOption;
            }
            else
            {
                currentMaterial.SetColor($"_Color_{colorOption.ToString()}", color);
            }
            
        }

        public void Initialize()
        {
            elements = new List<ModularElementOption>();

            elements = GetComponentsInChildren<ModularElementOption>(true).ToList();
            foreach (var element in elements)
            {
                element.Initialize();
            }

            currentMaterial = elements.Find(x => x.Type.Equals(bodyType)).Mat;
        }

        public void SetMaterialForAll()
        {
            elements.ForEach(x => x.SetMaterial(material));
            currentMaterial = material;
            color = currentMaterial.GetColor($"_Color_{colorOption.ToString()}");
        }

        public void SetMaterialForCurrentType()
        {
            var elementList = elements.FindAll(x => x.Type.Equals(bodyType));
            elementList.ForEach( x=> x.SetMaterial(material));
            currentMaterial = material;
            color = currentMaterial.GetColor($"_Color_{colorOption.ToString()}");
        }

        public void OnNext()
        {
            var elementList = elements.FindAll(x => x.Type.Equals(bodyType));
            elementList.ForEach( x=> x.Next());
        }
        
        public void OnPrevious()
        {
            var elementList = elements.FindAll(x => x.Type.Equals(bodyType));
            elementList.ForEach( x=> x.Previous());
        }

        public void OnSetDefault()
        {
            var elementList = elements.FindAll(x => x.Type.Equals(bodyType));
            elementList.ForEach( x=> x.SetDefault());
        }

        public void OnUnset()
        {
            var elementList = elements.FindAll(x => x.Type.Equals(bodyType));
            elementList.ForEach( x=> x.Unset());
        }

        public void OnUnsetAll()
        {
            elements.ForEach(x => x.Unset());
        }

        public void OnDefaultAll()
        {
            elements.ForEach(x => x.SetDefault());
        }
    }
}