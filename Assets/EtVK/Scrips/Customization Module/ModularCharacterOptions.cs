using System.Collections.Generic;
using System.Linq;
using EtVK.Utyles;
using Unity.Collections;
using UnityEngine;

namespace EtVK.Customization_Module
{
    public class ModularCharacterOptions : MonoBehaviour
    {
        [Header("Selected material to operate with, on type")]
        [SerializeField] private Material material;
        [SerializeField] private ModularOptions bodyType;
        [Header("The current material and color")]
        [SerializeField][ReadOnly] private Material currentMaterial;
        [SerializeField] private List<SerializableSet<ModularColorOptions, Color>> colorList;
        
        private List<ModularElementOption> elements = new();
        private ModularOptions lastCurrentType = ModularOptions.Head;

        private void OnValidate()
        {
            if (bodyType != lastCurrentType)
            {
                currentMaterial = elements.Find(x => x.Type.Equals(bodyType)).Mat;
                SetColors();
                lastCurrentType = bodyType;
            }
            else
            {
                colorList.ForEach(x=> currentMaterial.SetColor($"_Color_{x.GetKey().ToString()}", x.GetValue()));
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
            SetColors();
        }

        public void SetMaterialForAll()
        {
            elements.ForEach(x => x.SetMaterial(material));
            currentMaterial = material;
            SetColors();
        }

        public void SetMaterialForCurrentType()
        {
            var elementList = elements.FindAll(x => x.Type.Equals(bodyType));
            elementList.ForEach( x=> x.SetMaterial(material));
            currentMaterial = material;
            SetColors();
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


        private void SetColors()
        {
            var primary =  currentMaterial.GetColor($"_Color_{ModularColorOptions.Primary.ToString()}");
            var secondary =  currentMaterial.GetColor($"_Color_{ModularColorOptions.Secondary.ToString()}");
            var leatherPrimary = currentMaterial.GetColor($"_Color_{ModularColorOptions.Leather_Primary.ToString()}");
            var leatherSecondary = currentMaterial.GetColor($"_Color_{ModularColorOptions.Leather_Secondary.ToString()}");
            var metalPrimary = currentMaterial.GetColor($"_Color_{ModularColorOptions.Metal_Primary.ToString()}");
            var metalSecondary = currentMaterial.GetColor($"_Color_{ModularColorOptions.Metal_Secondary.ToString()}");
            var metalDark = currentMaterial.GetColor($"_Color_{ModularColorOptions.Metal_Dark.ToString()}");
            var hair = currentMaterial.GetColor($"_Color_{ModularColorOptions.Hair.ToString()}");
            var skin = currentMaterial.GetColor($"_Color_{ModularColorOptions.Skin.ToString()}");
            var stubble = currentMaterial.GetColor($"_Color_{ModularColorOptions.Stubble.ToString()}");
            var scar = currentMaterial.GetColor($"_Color_{ModularColorOptions.Scar.ToString()}");
            var eyes = currentMaterial.GetColor($"_Color_{ModularColorOptions.Eyes.ToString()}");
            var bodyArt = currentMaterial.GetColor($"_Color_{ModularColorOptions.BodyArt.ToString()}");

            colorList = new List<SerializableSet<ModularColorOptions, Color>>
            {
                new(ModularColorOptions.Primary, primary),
                new(ModularColorOptions.Secondary, secondary),
                new(ModularColorOptions.Leather_Primary, leatherPrimary),
                new(ModularColorOptions.Leather_Secondary, leatherSecondary),
                new(ModularColorOptions.Metal_Primary, metalPrimary),
                new(ModularColorOptions.Metal_Secondary, metalSecondary),
                new(ModularColorOptions.Metal_Dark, metalDark),
                new(ModularColorOptions.Hair, hair),
                new(ModularColorOptions.Skin, skin),
                new(ModularColorOptions.Stubble, stubble),
                new(ModularColorOptions.Scar, scar),
                new(ModularColorOptions.Eyes, eyes),
                new(ModularColorOptions.BodyArt, bodyArt),
            };
        }
    }
}