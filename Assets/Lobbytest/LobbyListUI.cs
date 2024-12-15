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
    private Lobby currentLobby;
    public TMP_InputField lobbyInput;
    public Player lobbyPlayer;
    public RelayCreation relayCreation;
    public Canvas LobbyUI;

    //This Part Written By Sean:

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if(Input.GetKeyDown(KeyCode.Space))
         {
             Create(refLobby.foundLobbies, refLobby.currentLobby, maxPlayers, isPrivate, refLobby.loggedInPlayer);
         }*/

        HandleLobbyPollForUpdates();
    }

    public void MakeLobby()
    {

        Create(refLobby.foundLobbies, refLobby.currentLobby, maxPlayers, isPrivate);

    }    

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
        currentLobby = await LobbyService.Instance.CreateLobbyAsync(
            lobbyName: newLobbyName,
            maxPlayers: maxPlayers,
            options: new CreateLobbyOptions()
            {
                Data = lobbyData,
                IsPrivate = isPrivate,
                Player = lobbyPlayer
            });

        Debug.Log($"Created new lobby {currentLobby.Name} ({currentLobby.Id}) {currentLobby.LobbyCode}");
        
        List<QueryOrder> queryOrdering = new List<QueryOrder>
        {
            new QueryOrder(true, QueryOrder.FieldOptions.AvailableSlots),
            new QueryOrder(false, QueryOrder.FieldOptions.Created),
            new QueryOrder(false, QueryOrder.FieldOptions.Name),
        };
        response = await LobbyService.Instance.QueryLobbiesAsync(new QueryLobbiesOptions()
        {
            Count = 20, // Override default number of results to return
           
            Order = queryOrdering,
        });
        
        PopulateLobbyUI(foundLobbies);

        lobbyCodeText.SetText("Lobby Code = " + currentLobby.LobbyCode);
    }

    public void refresh()
    {
        PopulateLobbyUI(refLobby.foundLobbies);
    }
    public void PopulateLobbyUI(List<Lobby> foundLobbies)
    {
       
        
        foundLobbies = response.Results;
        //For each lobby in the list
        foreach (Lobby item in foundLobbies) 
        { 
            //Instantiate the lobby prefab
            var panel =  Instantiate(panelUI);
            panel.transform.parent = panelParent;
            Debug.Log("lobby created");
        }
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
        currentLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(
            lobbyCode,
            options: new JoinLobbyByCodeOptions()
            {
                Player = lobbyPlayer
            });

        Debug.Log($"Joined lobby {currentLobby.Name} ({currentLobby.LobbyCode})");

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
            LobbyUI.gameObject.SetActive(false);
    
            string relayCode = await relayCreation.CreateRelay();

            Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(currentLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
                {
                    { "StartGame", new DataObject(DataObject.VisibilityOptions.Member, relayCode) }
                }
            });

            currentLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private void HandleLobbyPollForUpdates()
    {
        if (currentLobby.Data["StartGame"].Value != "0")
        {
            if (!IsLobbyHost())
            {
                relayCreation.JoinRelay(currentLobby.Data["StartGame"].Value);
            }
        }

        currentLobby = null;
    }

    public bool IsLobbyHost()
    {
        return currentLobby != null && currentLobby.HostId == AuthenticationService.Instance.PlayerId;
    }
}

