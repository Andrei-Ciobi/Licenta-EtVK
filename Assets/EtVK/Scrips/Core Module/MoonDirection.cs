using System;
using UnityEngine;

namespace EtVK.Core_Module
{
    [ExecuteAlways]
    public class MoonDirection : MonoBehaviour
    {
        void Update()
        {
            Shader.SetGlobalVector("_SunDirection", transform.forward);
        }
    }
}