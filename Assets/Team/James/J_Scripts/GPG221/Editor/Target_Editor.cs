using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JamesKilpatrick
{
    [CustomEditor(typeof(Target), true)]
    public class Target_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Move Taget") && Application.isPlaying)
            {
                Target targetObject;
                targetObject = target as Target;
                targetObject?.MoveTarget(); 

            }
        }
    }
}
