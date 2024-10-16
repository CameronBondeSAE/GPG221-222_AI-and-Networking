using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


namespace EB
{
    [CustomEditor(typeof(Spawner), editorForChildClasses: true)]

    public class Spawner_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Spawn"))
            {
                Spawner spawner;
                spawner = target as Spawner;
                spawner?.Spawn();
            }
        }
    }
}

