
using UnityEditor;
using UnityEngine;

namespace CameraCutScene
{
#if UNITY_EDITOR
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
#endif
    
}