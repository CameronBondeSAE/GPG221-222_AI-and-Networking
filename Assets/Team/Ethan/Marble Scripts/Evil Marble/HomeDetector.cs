using EB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EvilMarble"))
        {
            other.GetComponent<EvilMarbleSensor>().IsHome = true;

            Debug.Log("Marble is Home");
        }

        if (other.CompareTag("EvilMarble 2"))
        {
            other.GetComponent<EvilMarbleSensorss>().IsHome = true;
        }
    }
}
