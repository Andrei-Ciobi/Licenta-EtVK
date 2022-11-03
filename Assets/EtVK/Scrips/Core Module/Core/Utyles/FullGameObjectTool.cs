using System.Linq;
using UnityEngine;

namespace EtVK.Core.Utyles
{
    public class FullGameObjectTool : MonoBehaviour
    {
        public void SetFullGameState(bool state)
        {
            GetComponentsInChildren<IFullGameComponent>().ToList().ForEach(x=> x.StartFullGame = state);
        }
    }
}