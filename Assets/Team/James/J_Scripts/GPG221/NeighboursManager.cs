using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamesKilpatrick
{
    public class NeighboursManager : MonoBehaviour
    {
       
        public List<Transform> neighboursList;
        public Collider neighbourChecker;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            neighboursList.Add(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            neighboursList.Remove(other.transform);
        }

    }
}
