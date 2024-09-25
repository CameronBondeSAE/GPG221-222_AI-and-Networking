using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SeanA
{
    public class Moveforward : MonoBehaviour
    {

        Rigidbody body;
        [SerializeField] float forcemultiply = 15f;
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            body.AddRelativeForce(Vector3.forward * forcemultiply);
        }




    }
}
