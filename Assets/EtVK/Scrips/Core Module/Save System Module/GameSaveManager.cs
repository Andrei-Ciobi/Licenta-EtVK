using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using EtVK.Core;
using EtVK.Core.Utyles;
using UnityEngine;

namespace EtVK.Save_System_Module
{
    public class GameSaveManager : MonoSingleton<GameSaveManager>
    {
        private readonly string directory = "SaveFiles";
        private readonly string saveDataId = "file-data";
        private string currentSaveFile;
        private SaveFileData saveFileData;


        private void Awake()
        {
            InitializeSingleton();
        }

        [ContextMenu("Save")]
        public void Save()
        {
            if (string.IsNullOrEmpty(currentSaveFile))
            {
                Debug.Log("No current save file");
                return;
            }

            var state = LoadFile(currentSaveFile);
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

            var state = LoadFile(currentSaveFile);
            LoadState(state);
        }

        public void LoadSaveFileWithName(string fileName)
        {
            currentSaveFile = fileName + ".txt";
            Load();
        }

        public void DeleteSaveSlot(int slotId)
        {
            DeleteFile($"Slot_{slotId}.txt");
        }

        public void StartNewSaveSlot(int slotId)
        {
            currentSaveFile = $"Slot_{slotId}.txt";
            saveFileData.SlotId = slotId;
            saveFileData.GameLevel = GameLevel.One;
        }

        public void LoadSaveSlot(int slotId)
        {
            currentSaveFile = $"Slot_{slotId}.txt";
            Load();
        }

        public List<SaveFileData> GetSaveFilesData()
        {
            var data = new List<SaveFileData>();
            var fileNameList = GetAllSaveFileNames();

            if (fileNameList == null)
                return data;
            
            foreach (var fileName in fileNameList)
            {
                var state = LoadFile(fileName + ".txt");
                var saveData = (SaveFileData) state[saveDataId];
                data.Add(saveData);
            }
            
            return data;
        }

        public SaveFileData GetLastSavedFileData()
        {
            return GetSaveFilesData().OrderByDescending(x => x.LastSavedTime).FirstOrDefault();
        }

        public bool HasSaveFiles()
        {
            var saveFiles = GetSaveFilesData();

            if (saveFiles == null)
                return false;
            
            return saveFiles.Count > 0;
        }

        private List<string> GetAllSaveFileNames()
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

            return fileNames.OrderByDescending(x => x).ToList();
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

        private void DeleteFile(string fileName)
        {
            if (!Directory.Exists($"{Application.persistentDataPath}/{directory}"))
                return;
            
            var filePath = $"{Application.persistentDataPath}/{directory}/{fileName}";

            if(!File.Exists(filePath))
                return;
            
            File.Delete(filePath);
        }

        private Dictionary<string, object> LoadFile(string fileName)
        {
            var savePath = $"{Application.persistentDataPath}/{directory}/{fileName}";
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
            
            saveFileData.LastSavedTime = DateTime.Now;
            state[saveDataId] = saveFileData;
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

            if (state.TryGetValue(saveDataId, out var data))
            {
                saveFileData = (SaveFileData) data;
            }
        }
    }

    [System.Serializable]
    public struct SaveFileData
    {
        // ReSharper disable once InconsistentNaming
        public static SaveFileData Empty = new();
        public DateTime LastSavedTime { get; set; }
        public int SlotId { get; set; }
        public GameLevel GameLevel { get; set; }

        public static bool operator ==(SaveFileData first, SaveFileData second)
        {
            return first.SlotId == second.SlotId;
        }

        public static bool operator !=(SaveFileData first, SaveFileData second)
        {
            return first.SlotId != second.SlotId;
        }
        
        public bool Equals(SaveFileData other)
        {
            return SlotId == other.SlotId;
        }

        public override bool Equals(object obj)
        {
            return obj is SaveFileData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return SlotId;
        }
    }
}