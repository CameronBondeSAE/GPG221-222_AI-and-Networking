using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;



public class LobbyStartUp : MonoBehaviour
{
    // Inspector properties with initial values

    /// <summary>
    /// Used to set the lobby name in this example.
    /// </summary>
    public string newLobbyName = "LobbyHelloWorld" + Guid.NewGuid();

    /// <summary>
    /// Used to set the max number of players in this example.
    /// </summary>
    public int maxPlayers = 8;

    /// <summary>
    /// Used to determine if the lobby shall be private in this example.
    /// </summary>
    public bool isPrivate = false;

    // We'll only be in one lobby at once for this demo, so let's track it here
    private Lobby currentLobby;
    
    public Player lobbyPlayer;
    
    /// <summary>
    /// TASKS:
    /// Sign In Button
    /// Chose Color
    /// </summary>
    /// <returns></returns>
   
    
    public async void SignInPlayerAsync()
    {
        await UnityServices.InitializeAsync();

        // Log in a player for this game client
        lobbyPlayer = await GetPlayerFromAnonymousLoginAsync();

        // Add some data to our player
        // This data will be included in a lobby under players -> player.data
        lobbyPlayer.Data.Add("Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "No"));
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

    public async void SendDataToLobby()
    {
        // Populate the new lobby with some data; use indexes so it's easy to search for
        var lobbyData = new Dictionary<string, DataObject>()
        {
            ["Relay Code"] = new DataObject(DataObject.VisibilityOptions.Public, "Code"),
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

        Debug.Log($"Created new lobby {currentLobby.Name} ({currentLobby.Id})");
    }
    
    
}