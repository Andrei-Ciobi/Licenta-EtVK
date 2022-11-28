﻿using EtVK.Event_Module.Core;
using EtVK.Event_Module.Event_Types;
using UnityEngine;

namespace EtVK.Event_Module.Events
{
    [CreateAssetMenu(fileName = "New GameUi Event", menuName = "ScriptableObjects/Events/AbilityUi")]
    public class AbilityUiEvent : BaseGameEvent<AbilityUiData> { }
}