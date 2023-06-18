using UnityEngine;

namespace EtVK.Core
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance => instance;

        protected static T instance;


        protected void InitializeSingleton()
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
