using System.Collections;
using System.Collections.Generic;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class RelayCreation : MonoBehaviour
{
    public TextMeshProUGUI lobbyCodeText;
    public TMP_InputField lobbyInput;
    public string relayCode;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);

            relayCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log(relayCode);
            lobbyCodeText.SetText(relayCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();
            
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }


    public async void JoinRelay(string joincode)
    {
        try
        {
            Debug.Log("Joining relay with " + joincode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joincode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    public void ClickJoin()
    {
        JoinRelay(lobbyInput.text);
    }
}
