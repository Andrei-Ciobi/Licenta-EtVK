using UnityEngine;

namespace EtVK.Core.Utyles
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