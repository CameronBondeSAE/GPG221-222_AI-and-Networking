using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelViewModel : MonoBehaviour
{
    public int panelID;
    public TextMeshProUGUI lobbyName;
    public Button joinButton;
    public TextMeshProUGUI playerNameField;
	
	private void OnEnable()
	{
		// tmpInputField.onEndEdit.AddListener(ChangeName);
        
    }

	private void ChangeName(string arg0)
	{
		// fakeMenuPlayer.playerName = arg0;
		Debug.Log("Player name changed = "+arg0);
	}

	private void OnDisable()
	{
		// tmpInputField.onEndEdit.RemoveListener(ChangeName); 
	}
}
