using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using EtVK.Core;
using UnityEngine;

namespace EtVK.Save_System_Module
{
    public class GameSaveManager : MonoSingletone<GameSaveManager>
    {
        public readonly string directory = "SaveFiles";
        private string currentSaveFile;


        private void Awake()
        {
            InitializeSingletone();
        }

        [ContextMenu("Save")]
        public void Save()
        {
            if (string.IsNullOrEmpty(currentSaveFile))
            {
                Debug.Log("No current save file");
                return;
            }

            var state = LoadFile();
            SaveState(state);
            SaveFile(state);
        }

        [ContextMenu("Load")]
        public void Load()
        {
            if (string.IsNullOrEmpty(currentSaveFile))
            {
                Debug.Log("No current save file");
                return;
            }

            var state = LoadFile();
            LoadState(state);
        }

        public void LoadSaveFileWithName(string fileName)
        {
            currentSaveFile = fileName + ".txt";
            Load();
        }

        public void SetSaveFileName(string fileName)
        {
            currentSaveFile = fileName + ".txt";
        }

        public List<string> GetAllSaves()
        {
            var savePath = $"{Application.persistentDataPath}/{directory}";
            if (!Directory.Exists(savePath))
                return null;
            
            var filePaths = Directory.GetFiles(savePath).ToList();

            if (filePaths.Count == 0)
                return null;

            var fileNames = new List<string>();
            filePaths.ForEach(x =>
            {
                var fileName = x.Split("\\")[1];
                fileNames.Add(fileName.Split(".")[0]);
            });

            return fileNames;
        }


        private void SaveFile(object state)
        {
            if (!Directory.Exists($"{Application.persistentDataPath}/{directory}"))
            {
                Directory.CreateDirectory($"{Application.persistentDataPath}/{directory}");
            }

            var savePath = $"{Application.persistentDataPath}/{directory}/{currentSaveFile}";
            using (var stream = File.Open(savePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> LoadFile()
        {
            var savePath = $"{Application.persistentDataPath}/{directory}/{currentSaveFile}";
            if (!File.Exists(savePath))
            {
                Debug.Log("No save file found");
                return new Dictionary<string, object>();
            }

            using (var stream = File.Open(savePath, FileMode.Open))
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