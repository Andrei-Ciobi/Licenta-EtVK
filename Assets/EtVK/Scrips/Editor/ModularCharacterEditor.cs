using EtVK.Core_Module;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace

    [CustomEditor(typeof(ModularCharacterOptions))]
    public class ModularCharacterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var fow = (ModularCharacterOptions) target;
            
            EditorGUILayout.LabelField("Renderers Buttons");
            if (GUILayout.Button("Initialize"))
            {
                fow.Initialize();
            }
            
            //SET MATERIALS BUTTONS
            EditorGUILayout.LabelField("Set material for:");
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("All"))
            {
                fow.SetMaterialForAll();
            }
            if (GUILayout.Button("Current type"))
            {
                fow.SetMaterialForCurrentType();
            }
            GUILayout.EndHorizontal();
            
            // NEXT/PREVIOUS BUTTONS
            EditorGUILayout.LabelField("Shuffle buttons");
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Previous"))
            {
                fow.OnPrevious();
            }
            if (GUILayout.Button("Next"))
            {
                fow.OnNext();
            }
            GUILayout.EndHorizontal();
            
            
            // SET DEFAULT/UNSET BUTTONS
            EditorGUILayout.LabelField("Default/Unset");
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Default"))
            {
                fow.OnSetDefault();
            }
            if (GUILayout.Button("Unset"))
            {
                fow.OnUnset();
            }
            GUILayout.EndHorizontal();
            
            
            // SET DEFAULT/UNSET FOR ALL BUTTONS
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Default all"))
            {
                fow.OnDefaultAll();
            }
            if (GUILayout.Button("Unset all"))
            {
                fow.OnUnsetAll();
            }
            GUILayout.EndHorizontal();

        }
    }