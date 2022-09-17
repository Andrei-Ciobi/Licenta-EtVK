using UnityEngine;

namespace EtVK.Core
{
    public class MonoSingletone<T> : MonoBehaviour where T : MonoSingletone<T>
    {
        public static T Instance => instance;

        protected static T instance;


        protected void InitializeSingletone()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
