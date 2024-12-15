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
using Unity.Services.Lobbies.Models;

public class RelayCreation : MonoBehaviour
{
    public TextMeshProUGUI lobbyCodeText;
    public TMP_InputField lobbyInput;
    public TextMeshProUGUI joinCodeText;
    public string joinCode;
    public string relayCode;
    public Canvas LobbyUI;
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {
        joinCodeText.text = lobbyInput.text;
        relayCode = lobbyInput.text;
        joinCode = relayCode;
    }
    public async Task<string> StartHostWithRelay(int maxConnections = 5)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        Debug.Log(joinCode);
        lobbyCodeText.text = "Lobby Code = " + joinCode;
        return NetworkManager.Singleton.StartHost() ? joinCode : null;

        
    }


    public async Task<bool> StartClientWithRelay(string joinCode)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
        return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
    }

    public async void ClickJoin()
    {
        relayCode = lobbyInput.text;

        await StartClientWithRelay(relayCode);
    }

    public async void ClickStart()
    {
        await StartHostWithRelay();
    }

    public void UIGOBYE()
    {
        LobbyUI.gameObject.SetActive(false);
    }
}
