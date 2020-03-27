using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntranceManager))]
public class EntranceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EntranceManager grid = (EntranceManager)target;
        if (GUILayout.Button("Generate Grid"))
        {
            Undo.RecordObject(grid, "Created Grid");
            grid.CreateGrid();
            EditorUtility.SetDirty(grid);
        }
    }
}
