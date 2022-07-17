using UnityEngine;

namespace EtVK.Scrips.Core_Module
{
    public class AnimationEventManager : MonoBehaviour
    {
        public bool CanCombo => canCombo;

        private bool canCombo;


        public void SetCanCombo(int value)
        {
            var boolValue = value != 0;
            canCombo = boolValue;
        }
    }
}