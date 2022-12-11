using EtVK.Core.Utyles;
using UnityEngine;
using UnityEngine.UIElements;

namespace EtVK.Event_Module.Event_Types
{
    [System.Serializable] public struct CharacterCreationData
    {
        public ModularOptions BodyType { get; set; }
        public Color BodyColor { get; set; }
        public ModularColorOptions ColorType { get; set; }
        public VisualElement ImgContainer { get; set; }

        public CharacterCreationData(ModularOptions bodyType) : this()
        {
            BodyType = bodyType;
        }
        public CharacterCreationData(ModularOptions bodyType, VisualElement imgContainer) : this()
        {
            BodyType = bodyType;
            ImgContainer = imgContainer;
        }

        public CharacterCreationData(ModularOptions bodyType, Color bodyColor, ModularColorOptions colorType)
        {
            BodyType = bodyType;
            BodyColor = bodyColor;
            ColorType = colorType;
            ImgContainer = null;
        }
    }
}