using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JamesKilpatrick
{
    [CustomEditor(typeof(Pathfinder), true)]
    public class Pathfinder_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Generate New Path") && Application.isPlaying)
            {
                Pathfinder pathfinder;
                pathfinder = target as Pathfinder;
                pathfinder?.CreatePath(); 
            }
        }


    }
}
