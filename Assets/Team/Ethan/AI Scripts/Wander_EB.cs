using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace EB
{
    public class Wander_EB : MonoBehaviour
    {

        public Rigidbody rb;
        public float perlin = -10f;
        public float perlin2 = 10f;
        public float turnInterval = 5f; 

        private void Start()
        {
            StartCoroutine(TurnCoroutine());
        }

        private IEnumerator TurnCoroutine()
        {
            while (true) 
            {


                float torqueToApply = Random.Range(0f, 1f) > 0.5f ? perlin : perlin2;

                rb.AddRelativeTorque(0, torqueToApply, 0);

                yield return new WaitForSeconds(turnInterval);
            }
        }
    }
}

