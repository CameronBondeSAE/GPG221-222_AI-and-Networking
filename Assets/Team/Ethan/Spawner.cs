using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EB
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

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
            Instantiate(prefab, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }
}

