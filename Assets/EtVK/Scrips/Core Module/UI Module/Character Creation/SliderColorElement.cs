using System.Collections.Generic;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Utyles;
using UnityEngine;
using UnityEngine.UI;

namespace EtVK.UI_Module.Character_Creation
{
    public class SliderColorElement : MonoBehaviour
    {
        [SerializeField] private ModularOptions bodyType;
        [SerializeField] private ModularColorOptions colorType;
        [SerializeField] private CharacterCreationEvent colorChangeEvent;
        [SerializeField] private Slider redSlider;
        [SerializeField] private Slider greenSlider;
        [SerializeField] private Slider blueSlider;


        private ModularColorOptions currentColorType;
        private void Awake()
        {
            var sliders = new List<Slider> {redSlider, greenSlider, blueSlider};
            sliders.ForEach(x => x.onValueChanged.AddListener(OnColorChange));
            currentColorType = colorType;
        }

        public void SetColorType(int value)
        {
            
            var newColor = (ModularColorOptions) value;
            currentColorType = currentColorType == newColor ? colorType : newColor;
        }

        private void OnColorChange(float value)
        {
            var color = new Color(redSlider.value, greenSlider.value, blueSlider.value);
            colorChangeEvent.Invoke(new CharacterCreationData(bodyType, color, currentColorType));
        }
    }
}