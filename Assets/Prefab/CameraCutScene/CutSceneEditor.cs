using System;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace Prefab.CameraCutScene
{
    [CustomEditor(typeof(CutSceneCamera))]
    [CanEditMultipleObjects]
    public class CutSceneEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CutSceneCamera myTarget = (CutSceneCamera)target;
            if(GUILayout.Button("Set new position"))
            {
                myTarget.AddPosition();
            }
            EditorUtility.SetDirty(myTarget);
        }
    }
}