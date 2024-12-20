﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayGrid))]
public class PlayGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlayGrid grid = (PlayGrid)target;
        if(GUILayout.Button("Generate Grid"))
        {
            Undo.RecordObject(grid, "Created Grid");
            grid.CreateGrid();
            EditorUtility.SetDirty(grid);
        }
    }
}

