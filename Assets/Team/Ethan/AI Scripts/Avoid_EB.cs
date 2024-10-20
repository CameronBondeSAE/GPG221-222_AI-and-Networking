using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace EB
{
    public class Avoid_EB : MonoBehaviour
    {
        public Rigidbody rb;

        //[SerializeField] private float turnSpeedRight = 10f;
        [SerializeField] private float turnSpeedLeft = -10f;
        [SerializeField] private float distance = 2f;


        void Update()
        {
            bool hitsomething = Physics.Raycast(origin: transform.position, direction: transform.forward, out RaycastHit hit, distance);

            if (hitsomething)
            {
                
                
                 rb.AddRelativeTorque(0, turnSpeedLeft, 0);
                

            }


        }
    }
}

