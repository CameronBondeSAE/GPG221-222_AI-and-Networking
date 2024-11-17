using EB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeDetector : MonoBehaviour
{
    //when entering turn Ishome to true so we change state
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
