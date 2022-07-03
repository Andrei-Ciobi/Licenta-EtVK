using System.Collections.Generic;
using UnityEngine;

namespace EtVK
{
    [System.Serializable]
    public class SerializableSet<K,V>
    {
        [SerializeField] private K key;
        [SerializeField] private V value;


        public K GetKey()
        {
            return key;
        }

        public V GetValue()
        {
            return value;
        }

        public void AddKeyValue(K newKey, V newValue)
        {
            key = newKey;
            value = newValue;
        }
    }
}