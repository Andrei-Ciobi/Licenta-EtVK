using System;
using EtVK.Core.Utyles;
using EtVK.Event_Module.Events;
using EtVK.UI_Module.Core;
using UnityEngine;

namespace EtVK.UI_Module.Character_Creation
{
    [CreateAssetMenu(menuName = "ScriptableObjects/UiData/CharacterCreation")]
    public class CharacterCreationUiData : BaseUiData
    {
        [SerializeField] private CharacterCreationEvent onNext;
        [SerializeField] private CharacterCreationEvent onPrevious;
        [SerializeField] private CharacterCreationEvent onUnset;
        [SerializeField] private CharacterCreationEvent onColorChange;
        [SerializeField] private Material baseCustomizationMaterial;

        public CharacterCreationEvent OnNext => onNext;
        public CharacterCreationEvent OnPrevious => onPrevious;
        public CharacterCreationEvent OnUnset => onUnset;
        public CharacterCreationEvent OnColorChange => onColorChange;

        public Color GetBaseColor(ModularColorOptions colorOptions)
        {
            return baseCustomizationMaterial.GetColor($"_Color_{colorOptions.ToString()}");
        }
    }
}