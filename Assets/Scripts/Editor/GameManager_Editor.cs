using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager), true)]
public class GameManager_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Update Highscore"))
		{
			// ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
			GameManager gameManager;
			gameManager = target as GameManager;
			gameManager?.UpdateHighscoreRpc(Random.Range(0,100));
		}
	}
}
