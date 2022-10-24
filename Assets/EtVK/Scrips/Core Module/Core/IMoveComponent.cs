using UnityEngine;

namespace EtVK.Core
{
    public interface IMoveComponent
    {
        public void Move(Vector3 direction);
        public void Move(Vector3 direction, float speed);
    }
}