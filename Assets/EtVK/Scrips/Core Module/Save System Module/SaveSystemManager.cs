using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EtVK.Core;
using UnityEngine;

namespace EtVK.Save_System_Module
{
    public class SaveSystemManager : MonoSingletone<SaveSystemManager>
    {
        private string SavePath => $"{Application.persistentDataPath}/save.txt";


        private void Awake()
        {
            InitializeSingletone();
        }

        [ContextMenu("Save")]
        public void Save()
        {
            var state = LoadFile();
            SaveState(state);
            SaveFile(state);
        }

        [ContextMenu("Load")]
        public void Load()
        {
            var state = LoadFile();
            LoadState(state);
        }
        

        private void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);

            }
        }

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(SavePath))
            {
                Debug.Log("No save file found");
                return new Dictionary<string, object>();
            }

            using (var stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>) formatter.Deserialize(stream);
            }
        }

        private void SaveState(Dictionary<string, object> state)
        {
            foreach (var savableObject in FindObjectsOfType<SavableObject>())
            {
                state[savableObject.ID] = savableObject.SaveState();
            }
        }
        
        private void LoadState(Dictionary<string, object> state)
        {
            foreach (var savableObject in FindObjectsOfType<SavableObject>())
            {
                if (state.TryGetValue(savableObject.ID, out var savedState))
                {
                    savableObject.LoadState(savedState);
                }
            }
        }
    }
}