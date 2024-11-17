using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamesKilpatrick
{
    public class Spawner : MonoBehaviour
    {
        public float amount = 10;
        public float largeAmount = 50;
        public GameObject character;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Spawn10Character()
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(character, transform.position, Quaternion.Euler(0, Random.Range(0, 360),0));
            }
        }

        public void Spawn50Character()
        {
            for (int i = 0; i < largeAmount; i++)
            {
                Instantiate(character, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            }
        }
    }
}
