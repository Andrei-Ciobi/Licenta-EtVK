using UnityEngine;

namespace EtVK.Core_Module
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
