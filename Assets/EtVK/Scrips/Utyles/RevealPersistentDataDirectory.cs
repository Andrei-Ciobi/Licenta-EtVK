﻿using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace EtVK.Utyles
{
    public class RevealPersistentDataDirectory : MonoBehaviour
    {
        [MenuItem("Assets/Reveal Persistent Data Directory")]
        static void DoMenu() {
            Process process = new Process();
            process.StartInfo.FileName = ( (Application.platform == RuntimePlatform.WindowsEditor) ? "explorer.exe" : "open" );
            process.StartInfo.Arguments = "file://"+Application.persistentDataPath;
            process.Start();
        }
    }
}