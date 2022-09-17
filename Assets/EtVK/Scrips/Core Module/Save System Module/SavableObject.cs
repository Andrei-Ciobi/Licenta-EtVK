using System;
using System.Collections.Generic;
using UnityEngine;

namespace EtVK.Save_System_Module
{
    public class SavableObject : MonoBehaviour
    {
        [SerializeField] private string id;

        public string ID => id;

        private void Awake()
        {
            if (string.IsNullOrEmpty(id))
            {
                GenerateId();
            }
        }


        public object SaveState()
        {
            var state = new Dictionary<string, object>();
            foreach (var savable in GetComponentsInChildren<ISavable>())
            {
                state[savable.GetType().ToString()] = savable.SaveState();
            }

            return state;
        }

        public void LoadState(object state)
        {
            var stateDictionary = (Dictionary<string, object>) state;
            foreach (var savable in GetComponentsInChildren<ISavable>())
            {
                var typeName = savable.GetType().ToString();
                if (stateDictionary.TryGetValue(typeName, out var savedState))
                {
                    savable.LoadState(savedState);
                }
            }
        }

        public void GenerateId()
        {
            id = Guid.NewGuid().ToString();
        }
    }
}