using UnityEditor;
using UnityEngine;

namespace CameronBonde
{
	[CustomEditor(typeof(Spawner), true)]
	public class Spawner_Editor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Spawn") && Application.isPlaying)
			{
				// ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
				Spawner spawner;
				spawner = target as Spawner;
				spawner?.Spawn();
			}
		}
	}
}