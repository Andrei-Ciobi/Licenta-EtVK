using EtVK.Core.Utyles;
using UnityEditor;
using UnityEngine;


// ReSharper disable CheckNamespace

[CustomEditor(typeof(PrefabCreationTool), true)]
public class PrefabCreationToolEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var fow = (PrefabCreationTool) target;

        if (GUILayout.Button("Save"))
        {
            fow.Save();
        }
    }
}