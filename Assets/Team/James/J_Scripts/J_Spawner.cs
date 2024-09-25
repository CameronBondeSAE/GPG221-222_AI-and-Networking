using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Spawner : MonoBehaviour
{
    public float amount = 10;
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnCharacter()
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(character, transform.position, Quaternion.identity);
        }
    }
}
