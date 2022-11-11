using EtVK.Core.Utyles;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using UnityEngine;
using UnityEngine.UI;

namespace EtVK.UI_Module.Character_Creation
{
    public class ButtonClickElementColor : MonoBehaviour
    {
        [SerializeField] private ModularOptions bodyType;
        [SerializeField] private ModularColorOptions colorOptions;
        [SerializeField] private CharacterCreationEvent clickEvent;


        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnColorClick);
        }

        private void OnColorClick()
        {
            var color = GetComponent<Image>().color;
            clickEvent.Invoke(new CharacterCreationData(bodyType, color, colorOptions));
        }
    }
}