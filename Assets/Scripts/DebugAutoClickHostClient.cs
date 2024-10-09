using Unity.Netcode;
using UnityEngine;

#if UNITY_EDITOR
public class DebugAutoClickHostClient : MonoBehaviour
{
    public bool autoStartHostAndClient = true;

    void Awake()
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
#endif