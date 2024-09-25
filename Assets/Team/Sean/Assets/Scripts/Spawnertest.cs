using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SeanA{
    public class Spawnertest : MonoBehaviour
    {

        [SerializeField] Object prefab;
        [SerializeField] int spawnamount = 5;
        [SerializeField] float spawnRadius = 10;
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < spawnamount; i++)
            {
                Instantiate(prefab, transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)), Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
