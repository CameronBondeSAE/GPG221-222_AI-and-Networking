using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DebugAutoClickHostClient_EB : MonoBehaviour
{
    public bool autoStartHostAndClient = true;

    void Start()
    {
        if (autoStartHostAndClient)
        {
            if (!ParrelSync.ClonesManager.IsClone())
            {
                GetComponent<NetworkManager>().StartHost();
            }
            else
            {
                GetComponent<NetworkManager>().StartClient();
            }
        }
    }
}
