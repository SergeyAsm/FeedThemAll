using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SettingsData))]
public class SettingsEditorUI : Editor
{
    public override void OnInspectorGUI()
    {
        var realTarget = (SettingsData)target;

        if (GUILayout.Button("SaveToJson"))
        {
            realTarget.SaveToJson();
        }
        if (GUILayout.Button("LoadFromJson"))
        {
            realTarget.LoadFromJson();
            GUI.FocusControl("LoadFromJson");
        }
        DrawDefaultInspector();
    }
}