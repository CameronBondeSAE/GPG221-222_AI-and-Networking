using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace JamesKilpatrick
{
    public class Lobby_System : MonoBehaviour
    {
        public string lobbyName = "new lobby";
        public int maxPlayers = 4;
        CreateLobbyOptions options = new CreateLobbyOptions();
       // private bool IsServerPublic = false;
        private Lobby currentLobby;


        // Start is called before the first frame update
        void Start()
        {
            try
            {
                //CreateLobbyAsync(lobbyName, maxPlayers);
            }
            catch
            {

            }
        }

        // Update is called once per frame
        void Update()
        {

        }


        async void CreateLobbyAsync()
        {
            Lobby Lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
        }
        
    }
}
