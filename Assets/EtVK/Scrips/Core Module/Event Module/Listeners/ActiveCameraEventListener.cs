﻿using EtVK.Event_Module.Custom_Events;
using EtVK.Event_Module.Event_Types;
using EtVK.Event_Module.Events;

namespace EtVK.Event_Module.Listeners
{
    public class ActiveCameraEventListener : BaseGameEventListener<ActiveCamera, ActiveCameraEvent, CustomActiveCameraEvent>{}

}