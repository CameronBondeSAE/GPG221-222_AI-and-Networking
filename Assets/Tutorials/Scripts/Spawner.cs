using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameronBonde
{

	public class Spawner : MonoBehaviour
	{
		public List<GameObject> Characters;

		public int amount = 10;

		// Start is called before the first frame update
		void Start()
		{
			SpawnMany();
		}

		public void SpawnMany()
		{
			for (int i = 0; i < amount; i++)
			{
				Spawn();
			}
		}

		public void Spawn()
		{
			Instantiate(Characters[Random.Range(0, Characters.Count)], transform.position,
			            Quaternion.Euler(0, Random.Range(0, 360), 0));
		}
	}
}