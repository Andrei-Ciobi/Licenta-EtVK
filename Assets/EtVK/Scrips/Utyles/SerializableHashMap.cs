using System.Collections.Generic;
using UnityEngine;

namespace EtVK.Utyles
{
    [System.Serializable]
    public class SerializableHashMap<K, V>
    {
        [SerializeField] private List<SerializableSet<K, V>> customHashMap;

        public int Length => customHashMap.Count;
        public V this[K key]
        {
            get => customHashMap.Find((set) => set.GetKey().Equals(key)).GetValue();
        }

        public void Add(K key, V value)
        {
            customHashMap.Add(new SerializableSet<K, V>(key, value));
        }

        public List<K> GetKeys()
        {
            var keyList = new List<K>();

            foreach (var set in customHashMap)
            {
                keyList.Add(set.GetKey());
            }

            return keyList;
        }
    }
}