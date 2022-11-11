using EtVK.Core.Manager;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var fow = (GameManager) target;

        if (GUILayout.Button("Configure for full game"))
        {
            fow.SetFullGameState(true);
        }
        
        if (GUILayout.Button("Configure for partial game"))
        {
            fow.SetFullGameState(false);
        }
        
    }
}