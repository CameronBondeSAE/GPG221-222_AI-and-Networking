using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Random = UnityEngine.Random;
using Unity.Services.Relay.Models;
public class LobbyListUI : MonoBehaviour
{
    public Transform panelParent;
    [SerializeField] private RefLobbyHelloWorld refLobby;
    [SerializeField] GameObject panelUI;
    public string newLobbyName = "LobbyHelloWorld" + Guid.NewGuid();
    public string lobbyNameText;
    private QueryResponse response;
    public int maxPlayers = 8;
    public bool isPrivate = false;
    public TextMeshProUGUI lobbyCodeText;
    public TMP_InputField lobbyInput;
    public Player lobbyPlayer;
    public RelayCreation relayCreation;
    public Canvas LobbyUI;
    public Lobby hostLobby;
    public Lobby currentLobby;
    public bool hasGameStarted = false;

    //This Part Written By Sean:

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleLobbyPollForUpdates();
    }

    public void MakeLobby()
    {

        /// Create(refLobby.foundLobbies, refLobby.currentLobby, maxPlayers, isPrivate);
        CreateLobby();
    }
    /* SEANS OLD LOBBY CREATION (JAMES REPLACED)
    public async Task Create(List<Lobby> foundLobbies, Lobby currentLobby, int maxPlayers, bool isPrivate)
    {
    
    // Populate the new lobby with some data; use indexes so it's easy to search for
    var lobbyData = new Dictionary<string, DataObject>()
    {
        ["StartGame"] = new DataObject(DataObject.VisibilityOptions.Member, "0"),
        ["Test"] = new DataObject(DataObject.VisibilityOptions.Public, "true", DataObject.IndexOptions.S1),
        ["GameMode"] = new DataObject(DataObject.VisibilityOptions.Public, "Marble Race", DataObject.IndexOptions.S2)

    };

    // Create a new lobby
    Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(
        lobbyName: newLobbyName,
        maxPlayers: maxPlayers,
        options: new CreateLobbyOptions()
        {
            Data = lobbyData,
            IsPrivate = isPrivate,
            Player = lobbyPlayer
        });

    hostLobby = lobby;
    currentLobby = hostLobby;

    Debug.Log($"Created new lobby {lobby.Name} ({lobby.Id}) {lobby.LobbyCode}");
    */

    public async void CreateLobby()
    {
        try
        {
            string lobbyName = "lobby";
            int maxPlayers = 4;
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
                Data = new Dictionary<string, DataObject>
                {
                    {"GameMode", new DataObject(DataObject.VisibilityOptions.Public, "MarbleGame") },
                    {"StartGame", new DataObject(DataObject.VisibilityOptions.Member, "0") }
            }
            };


            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createLobbyOptions);

            hostLobby = lobby;
            currentLobby = hostLobby;

            Debug.Log("Lobby Created" + lobby.Name + ", " + lobby.MaxPlayers + ", " + lobby.Id + ", " + lobby.LobbyCode);          

            lobbyCodeText.SetText("Lobby Code = " + hostLobby.LobbyCode);

        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e.Message);
        }

    }

    private async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 20, // Override default number of results to return

                Filters = new List<QueryFilter>
                {
                     new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };


            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            foreach (Lobby lobby in queryResponse.Results)
            {
                //Instantiate the lobby prefab
                var panel = Instantiate(panelUI);
                panel.transform.parent = panelParent;
                Debug.Log("lobby's Refreshed");
            }


        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public void refresh()
    {
        ListLobbies();
    }

    //This Part Written By James:
    public async void SignInPlayerAsync()
    {
        await UnityServices.InitializeAsync();

        // Log in a player for this game client
        Player loggedInPlayer = await GetPlayerFromAnonymousLoginAsync();

        loggedInPlayer = lobbyPlayer;

        // Add some data to our player
        // This data will be included in a lobby under players -> player.data
        //lobbyPlayer.Data.Add("Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "No"));

        ListLobbies();
    }

    // Log in a player using Unity's "Anonymous Login" API and construct a Player object for use with the Lobbies APIs
    static async Task<Player> GetPlayerFromAnonymousLoginAsync()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log($"Trying to log in a player ...");

            // Use Unity Authentication to log in
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                throw new InvalidOperationException(
                    "Player was not signed in successfully; unable to continue without a logged in player");
            }
        }

        Debug.Log("Player signed in as " + AuthenticationService.Instance.PlayerId);

        // Player objects have Get-only properties, so you need to initialize the data bag here if you want to use it
        return new Player(AuthenticationService.Instance.PlayerId,
            data: new Dictionary<string, PlayerDataObject>());
    }

    public async void JoinLobby(string lobbyCode)
    {
        // Try to join the lobby
        // Player is optional because the service can pull the player data from the auth token
        // However, if your player has custom data, you will want to pass the Player object into this call
        // This will save you having to do a Join call followed by an UpdatePlayer call
        Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(
            lobbyCode,
            options: new JoinLobbyByCodeOptions()
            {
                Player = lobbyPlayer
            });

        currentLobby = lobby;


        Debug.Log($"Joined lobby {lobby.Name} ({lobby.LobbyCode})");

        // You can also join via a Lobby Code instead of a lobby ID
        // Lobby Codes are a short, unique codes that map to a specific lobby ID
        // EX:
        // currentLobby = await LobbyService.Instance.JoinLobbyByCodeAsync("myLobbyJoinCode");
    }

    public void ClickJoin()
    {
        JoinLobby(lobbyInput.text);
    }

    public async void StartGame()
    {
        try
        {
           // string relayCode = await relayCreation.CreateRelay();

            /* 
             currentLobby.Data["StartGame"] =
                 new DataObject(DataObject.VisibilityOptions.Member, relayCode);

             
             currentLobby = await LobbyService.Instance.UpdateLobbyAsync(
                 currentLobby.Name,
                 options: new UpdateLobbyOptions()
                 {
                     Data = currentLobby.Data,
                 }
                 );
             */

            Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(currentLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
                {
                   // { "StartGame", new DataObject(DataObject.VisibilityOptions.Member, relayCode) }
                }
            });
            currentLobby = lobby;
            Debug.Log(currentLobby.Data);

            LobbyUI.gameObject.SetActive(false);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
      
    }

    private void HandleLobbyPollForUpdates()
    {
        if (currentLobby != null)
        {
            if (currentLobby.Data["StartGame"].Value != "0")
            {
              //  relayCreation.JoinRelay(currentLobby.Data["StartGame"].Value);
            }
        }
        currentLobby = null;
    }

    public bool IsLobbyHost()
    {
        return currentLobby != null && currentLobby.HostId == AuthenticationService.Instance.PlayerId;
    }

    //If Host Disconnects Switch to New Hots
    private async void MigrateLobbyHost()
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                HostId = currentLobby.Players[1].Id
            });
            currentLobby = hostLobby;

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}

