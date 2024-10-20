using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EB
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

        public int amount = 1;

        public void SpawnMany()
        {
            for (int i = 0; i < amount; i++)
            {
                Spawn();
            }
        }

        public void Spawn()
        {
            Instantiate(prefab, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }
}

