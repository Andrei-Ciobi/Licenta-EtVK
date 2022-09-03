using EtVK.Save_System_Module;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace


[CustomEditor(typeof(SavableObject))]
public class SavableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var fow = (SavableObject) target;

        if (GUILayout.Button("Generate ID"))
        {
            fow.GenerateId();
        }
        
    }
}