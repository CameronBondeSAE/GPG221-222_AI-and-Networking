using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace EB
{
    public class EBHoldButton : NetworkBehaviour
    {
        [SerializeField] float HoldTime = 0f;
        [SerializeField] float buttonTimer = 5f;
        [SerializeField] bool playerInRange = false;
        [SerializeField] Renderer buttonRenderer;
        [SerializeField] Color buttonStart = Color.red;
        [SerializeField] Color buttonEnd = Color.green;

        private NetworkVariable<Color> currentColor = new NetworkVariable<Color>(Color.red,
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

        private void Start()
        {
            buttonRenderer = GetComponent<Renderer>();

            currentColor.OnValueChanged += OnColorChanged;
            buttonRenderer.material.color = currentColor.Value;
        }


        private void FixedUpdate()
        {

            if(playerInRange && Input.GetKey(KeyCode.E))
            {
                Debug.Log($"{NetworkManager.Singleton.LocalClientId} is pressing E.");
                HoldTime += Time.deltaTime;
                ChangeColour(HoldTime / buttonTimer);

                if (HoldTime >= buttonTimer)
                {
                    ButtonPressedServerRpc();
                    ResetTimer();
                }
            }
            else if (Input.GetKeyUp(KeyCode.E) || !playerInRange)
            {
                ResetTimer();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
            {
                playerInRange = true;
                Debug.Log($"{NetworkManager.Singleton.LocalClientId} entered button range.");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void ButtonPressedServerRpc(ServerRpcParams rpcParams = default)
        {
            Debug.Log("Button Pressed on Server");
        }

        private void ResetTimer()
        {
            HoldTime = 0f;
            ChangeColour(0f);
        }

        private void ChangeColour(float progress)
        {
            Color newColor = Color.Lerp(buttonStart, buttonEnd, Mathf.Clamp01(progress));
            UpdateColorServerRpc(newColor);
        }

        [ServerRpc(RequireOwnership = false)]
        private void UpdateColorServerRpc(Color color, ServerRpcParams rpcParams = default)
        {
            currentColor.Value = color; 
        }

        private void OnColorChanged(Color oldColor, Color newColor)
        {
            buttonRenderer.material.color = newColor; 
        }

    }
}

