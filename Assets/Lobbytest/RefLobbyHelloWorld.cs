using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Random = UnityEngine.Random;

/// <summary>
/// A simple "Hello World" style example using Lobby, exercising most of the Lobby APIs.
///
/// SETUP:
///  1. Attach this script to a GameObject in scene
///  2. Sign in to your Unity developer account
///  3. Link your project to a Lobby-enabled cloud project
///  4. Enter play mode and observe the debug logs
///
/// </summary>
public class RefLobbyHelloWorld : MonoBehaviour
{
    public LobbyListUI listUI;

    public Player loggedInPlayer;

    public List<Lobby> foundLobbies;
    // Inspector properties with initial values

    /// <summary>
    /// Used to set the lobby name in this example.
    /// </summary>


    /// <summary>
    /// Used to set the max number of players in this example.
    /// </summary>


    /// <summary>
    /// Used to determine if the lobby shall be private in this example.
    /// </summary>


    // We'll only be in one lobby at once for this demo, so let's track it here
    public Lobby currentLobby;

    async void Start()
    {
        try
        {
            await ExecuteLobbyDemoAsync();

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        await CleanupDemoLobbyAsync();

        Debug.Log("Demo complete!");
    }

    // Clean up the lobby we're in if we're the host
    async Task CleanupDemoLobbyAsync()
    {
        var localPlayerId = AuthenticationService.Instance.PlayerId;

        // This is so that orphan lobbies aren't left around in case the demo fails partway through
        if (currentLobby != null && currentLobby.HostId.Equals(localPlayerId))
        {
            await LobbyService.Instance.DeleteLobbyAsync(currentLobby.Id);
            Debug.Log($"Deleted lobby {currentLobby.Name} ({currentLobby.Id})");
        }
    }

    // A basic demo of lobby functionality
    async Task ExecuteLobbyDemoAsync()
    {
        await UnityServices.InitializeAsync();

        // Log in a player for this game client
        loggedInPlayer = await GetPlayerFromAnonymousLoginAsync();

        // Add some data to our player
        // This data will be included in a lobby under players -> player.data
        loggedInPlayer.Data.Add("Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "No"));

        // Query for existing lobbies

        // Use filters to only return lobbies which match specific conditions
        // You can only filter on built-in properties (Ex: AvailableSlots) or indexed custom data (S1, N1, etc.)
        // Take a look at the API for other built-in fields you can filter on
        List<QueryFilter> queryFilters = new List<QueryFilter>
        {
            // Let's search for games with open slots (AvailableSlots greater than 0)
            new QueryFilter(
                field: QueryFilter.FieldOptions.AvailableSlots,
                op: QueryFilter.OpOptions.GT,
                value: "0"),

            // Let's add some filters for custom indexed fields
            new QueryFilter(
                field: QueryFilter.FieldOptions.S1, // S1 = "Test"
                op: QueryFilter.OpOptions.EQ,
                value: "true"),

            new QueryFilter(
                field: QueryFilter.FieldOptions.S2, // S2 = "GameMode"
                op: QueryFilter.OpOptions.EQ,
                value: "ctf"),

            // Example "skill" range filter (skill is a custom numeric field in this example)
            new QueryFilter(
                field: QueryFilter.FieldOptions.N1, // N1 = "Skill"
                op: QueryFilter.OpOptions.GT,
                value: "0"),

            new QueryFilter(
                field: QueryFilter.FieldOptions.N1, // N1 = "Skill"
                op: QueryFilter.OpOptions.LT,
                value: "51"),
        };

        // Query results can also be ordered
        // The query API supports multiple "order by x, then y, then..." options
        // Order results by available player slots (least first), then by lobby age, then by lobby name
        List<QueryOrder> queryOrdering = new List<QueryOrder>
        {
            new QueryOrder(true, QueryOrder.FieldOptions.AvailableSlots),
            new QueryOrder(false, QueryOrder.FieldOptions.Created),
            new QueryOrder(false, QueryOrder.FieldOptions.Name),
        };

        // Call the Query API
        QueryResponse response = await LobbyService.Instance.QueryLobbiesAsync(new QueryLobbiesOptions()
        {
            Count = 20, // Override default number of results to return
            Filters = queryFilters,
            Order = queryOrdering,
        });

        foundLobbies = response.Results;





        //await listUI.Create(foundLobbies,currentLobby, maxPlayers, isPrivate, loggedInPlayer);

        listUI.PopulateLobbyUI(foundLobbies);


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
                throw new InvalidOperationException("Player was not signed in successfully; unable to continue without a logged in player");
            }
        }

        Debug.Log("Player signed in as " + AuthenticationService.Instance.PlayerId);

        // Player objects have Get-only properties, so you need to initialize the data bag here if you want to use it
        return new Player(AuthenticationService.Instance.PlayerId, null, data: new Dictionary<string, PlayerDataObject>());
    }
}
