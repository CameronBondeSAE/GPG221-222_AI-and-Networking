using Codice.CM.SEIDInfo;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace JamesKilpatrick
{
    [CustomEditor(typeof(Spawner), true)]

    public class Spawner_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //isPlaying makes sure when button is pressed it doesn't add new characters into the editor when not in play mode
            if (GUILayout.Button("Spawn") && Application.isPlaying)
            {
                Spawner spawner;
                spawner = target as Spawner;
                spawner?.SpawnCharacter();
            }
        }
        
    }
}
