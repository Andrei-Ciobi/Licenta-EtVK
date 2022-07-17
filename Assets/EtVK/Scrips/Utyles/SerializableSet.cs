using UnityEngine;

namespace EtVK.Scrips.Utyles
{
    [System.Serializable]
    public class SerializableSet<K,V>
    {
        [SerializeField] private K key;
        [SerializeField] private V value;

        public SerializableSet()
        {
        }

        public SerializableSet(K key, V value)
        {
            this.key = key;
            this.value = value;
        }


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