using System;
using System.Collections.Generic;
using System.Linq;
using EtVK.Event_Module.Event_Types;
using EtVK.Utyles;
using UnityEngine;

namespace EtVK.Customization_Module
{
    public class CharacterCustomization : MonoBehaviour
    {
        private List<ModularElementOption> elements = new();

        private void Awake()
        {
            Initialize();
        }

        public void OnNext(CharacterCreationData data)
        {
            var elementList = elements.FindAll(x => x.Type.Equals(data.BodyType));
            elementList.ForEach(x => x.Next());
        }

        public void OnPrevious(CharacterCreationData data)
        {
            var elementList = elements.FindAll(x => x.Type.Equals(data.BodyType));
            elementList.ForEach(x => x.Previous());
        }

        public void OnSetDefault(CharacterCreationData data)
        {
            var elementList = elements.FindAll(x => x.Type.Equals(data.BodyType));
            elementList.ForEach(x => x.SetDefault());
        }

        public void OnUnset(CharacterCreationData data)
        {
            var elementList = elements.FindAll(x => x.Type.Equals(data.BodyType));
            elementList.ForEach(x => x.Unset());
        }

        public void SetColor(CharacterCreationData data)
        {
            if (data.BodyType == ModularOptions.All)
            {
                elements.ForEach(x => { x.Mat.SetColor($"_Color_{data.ColorType.ToString()}", data.BodyColor); });
                return;
            }

            var elementList = elements.FindAll(x => x.Type.Equals(data.BodyType));
            elementList.ForEach(x => { x.Mat.SetColor($"_Color_{data.ColorType.ToString()}", data.BodyColor); });
        }


        private void Initialize()
        {
            elements = new List<ModularElementOption>();

            elements = GetComponentsInChildren<ModularElementOption>(true).ToList();
            foreach (var element in elements)
            {
                element.Initialize();
            }

            // currentMaterial = elements.Find(x => x.Type.Equals(bodyType)).Mat;
            // SetColors();
            // SetFloats();
        }
    }
}