using UnityEngine;

namespace EtVK.Core.Manager
{
    public class BlockingManager : MonoBehaviour
    {
        public int BlockingLayer => blockingLayer;
        public bool IsBlocking => isBlocking;

        private bool isBlocking;
        private int percentageAbsorption;
        private int blockingLayer;


        public bool CheckBlockingStatus(float direction)
        {
            if (!isBlocking)
                return false;
            
            return direction switch
            {
                >= 145 and <= 180 => true,
                <= -145 and >= -180 => true,
                _ => false
            };
        }

        public float CalculateNewDamage(float damage)
        {
            return damage - (damage * percentageAbsorption) / 100;
        }

        public void SetAbsorption(int value)
        {
            percentageAbsorption = value;
        }
        
        public void SetBlocking(bool value, int layer)
        {
            isBlocking = value;
            blockingLayer = layer;
        }
        
    }
}