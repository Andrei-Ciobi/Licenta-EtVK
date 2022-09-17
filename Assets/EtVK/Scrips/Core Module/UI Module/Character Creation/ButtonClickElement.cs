using System;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Utyles;
using UnityEngine;
using UnityEngine.UI;

namespace EtVK.UI_Module.Character_Creation
{
    public class ButtonClickElement : MonoBehaviour
    {
        [SerializeField] private ModularOptions bodyType;
        [SerializeField] private CharacterCreationEvent clickEvent;


        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener( () => clickEvent.Invoke(new CharacterCreationData(bodyType)));
        }
    }
}