﻿using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;
using EtVK.Utyles;

namespace EtVK.Event_Module.Listeners
{
    public class CharacterCreationEventListener : BaseGameEventListener<CharacterCreationData, CharacterCreationEvent, CustomCharacterCreationEvent> { }

}