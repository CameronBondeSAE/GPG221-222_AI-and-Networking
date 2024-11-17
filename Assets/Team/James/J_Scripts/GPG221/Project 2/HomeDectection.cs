using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamesKilpatrick;


public class HomeDectection : MonoBehaviour
{
    //Trigger used to detect when Evil Marble enters Home Area. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EvilMarble"))
        {
            other.GetComponent<EvilMarbleSensor>().IsHome = true;

            Debug.Log("Evil Marble is home.");
        }

        if (other.CompareTag("ChasingEvilMarble"))
        {
            other.GetComponent<ChasingEvilMarbleSensor>().IsHome = true;

            Debug.Log("Chasing Evil Marble is home");
        }
    }
}

