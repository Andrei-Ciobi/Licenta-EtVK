using EtVK.Customization_Module;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace

[CustomEditor(typeof(SaveCustomization), true)]
public class SaveCustomizationEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var fow = (SaveCustomization) target;

        if (GUILayout.Button("Save"))
        {
            fow.Save();
        }
    }
}