using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeanA
{
    public class Avoid : MonoBehaviour
    {
        [SerializeField] float scandistance;
        Rigidbody rigidbody;

        bool avoidtarget;
        [SerializeField] float forcemultiplier = 100f;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            avoidtarget = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, scandistance);
            if (avoidtarget)
            {
                rigidbody.AddRelativeTorque(Vector3.up * forcemultiplier, ForceMode.Force);
            }
        }

    }
}