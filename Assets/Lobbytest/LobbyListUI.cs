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
using Random = UnityEngine.Random;
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
    }

    public void MakeLobby()
    {
        Create(refLobby.foundLobbies, refLobby.currentLobby, maxPlayers, isPrivate, refLobby.loggedInPlayer);


    }
    
    

    

    public async Task Create(List<Lobby> foundLobbies, Lobby currentLobby, int maxPlayers, bool isPrivate, Player loggedInPlayer)
    {

        
        // Populate the new lobby with some data; use indexes so it's easy to search for
        var lobbyData = new Dictionary<string, DataObject>()
        {
            ["Test"] = new DataObject(DataObject.VisibilityOptions.Public, "true", DataObject.IndexOptions.S1),
            ["GameMode"] = new DataObject(DataObject.VisibilityOptions.Public, "ctf", DataObject.IndexOptions.S2),
            ["Skill"] = new DataObject(DataObject.VisibilityOptions.Public, Random.Range(1, 51).ToString(),
                DataObject.IndexOptions.N1),
            ["Rank"] = new DataObject(DataObject.VisibilityOptions.Public, Random.Range(1, 51).ToString()),
        };

        // Create a new lobby
        currentLobby = await LobbyService.Instance.CreateLobbyAsync(
            lobbyName: newLobbyName,
            maxPlayers: maxPlayers,
            options: new CreateLobbyOptions()
            {
                Data = lobbyData,
                IsPrivate = isPrivate,
                Player = loggedInPlayer
            });

        Debug.Log($"Created new lobby {currentLobby.Name} ({currentLobby.Id})");
        
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




}

