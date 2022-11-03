using UnityEngine;

namespace EtVK.Core.Utyles
{
    public interface IMoveComponent
    {
        public void Move(Vector3 direction);
        public void Move(Vector3 direction, float speed);
    }
}