using System.Collections.Generic;
using System.Linq;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.Save_System_Module;
using UnityEngine;

namespace EtVK.Customization_Module
{
    public class CharacterCustomization : MonoBehaviour, ISavable
    {
        [SerializeField] private Material defaultMaterial;
        private List<ModularElementOption> elements = new();

        private Material newMat;

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

        public object SaveState()
        {
            return new SaveData(newMat, elements);
        }

        public void LoadState(object state)
        {
            var saveData = (SaveData) state;
            foreach (var item in saveData.Colors)
            {
                newMat.SetColor($"_Color_{item.Key.ToString()}",
                    new Color(item.Value.Red, item.Value.Green, item.Value.Blue));
            }

            foreach (var item in elements)
            {
                if (saveData.HeadComponents.TryGetValue(item.Type, out var elementName))
                {
                    item.SetActiveByName(elementName);
                }
            }
        }

        private void Initialize()
        {
            elements = new List<ModularElementOption>();

            elements = GetComponentsInChildren<ModularElementOption>(true).ToList();
            newMat = new Material(defaultMaterial.shader);
            newMat.CopyPropertiesFromMaterial(defaultMaterial);


            foreach (var element in elements)
            {
                element.Initialize();
                element.SetMaterial(newMat);
            }
        }

        [System.Serializable]
        private struct SaveData
        {
            public Dictionary<ModularColorOptions, SerializableColor> Colors { get; set; }
            public Dictionary<ModularOptions, string> HeadComponents { get; set; }

            public SaveData(Material material, List<ModularElementOption> headComponents) : this()
            {
                var hair = material.GetColor($"_Color_{ModularColorOptions.Hair.ToString()}");
                var skin = material.GetColor($"_Color_{ModularColorOptions.Skin.ToString()}");
                var stubble = material.GetColor($"_Color_{ModularColorOptions.Stubble.ToString()}");
                var scar = material.GetColor($"_Color_{ModularColorOptions.Scar.ToString()}");
                var bodyArt = material.GetColor($"_Color_{ModularColorOptions.BodyArt.ToString()}");

                Colors = new Dictionary<ModularColorOptions, SerializableColor>
                {
                    {ModularColorOptions.Hair, new SerializableColor(hair)},
                    {ModularColorOptions.Skin, new SerializableColor(skin)},
                    {ModularColorOptions.Stubble, new SerializableColor(stubble)},
                    {ModularColorOptions.Scar, new SerializableColor(scar)},
                    {ModularColorOptions.BodyArt, new SerializableColor(bodyArt)}
                };

                HeadComponents = new Dictionary<ModularOptions, string>();

                foreach (var component in headComponents)
                {
                    HeadComponents.Add(component.Type, component.GetCurrentElement()?.gameObject.name ?? "none");
                }
            }
        }
    }
}