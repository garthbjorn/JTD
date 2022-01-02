using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];


    private void Start()
    {
        NetworkManagerTD.ClientOnConnected += HandleClientConnected;
        PlayerTD.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerStateUpdated;
        PlayerTD.ClientOnInfoUpdated += ClientHandleInfoUpdated;
    }
    private void OnDestroy()
    {
        NetworkManagerTD.ClientOnDisconnected -= HandleClientConnected;
        PlayerTD.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerStateUpdated;
        PlayerTD.ClientOnInfoUpdated += ClientHandleInfoUpdated;

    }
    private void ClientHandleInfoUpdated()
    {
        List<PlayerTD> players = ((NetworkManagerTD)NetworkManager.singleton).players;
        
        for(int i = 0; i< players.Count;i++)
        {
            playerNameTexts[i].text = players[i].GetDisplayName();
        }
        for (int i = players.Count; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Open:";
        }
    }
    private void HandleClientConnected()
    {
        Debug.Log("HandleClientConnected");
        lobbyUI.SetActive(true);
    }

    private void AuthorityHandlePartyOwnerStateUpdated(bool state)
    {
        startGameButton.gameObject.SetActive(state);
    }

    public void StartGame()
    {
        Debug.Log("LobbyMenu Start");

        NetworkClient.connection.identity.GetComponent<PlayerTD>().CmdStartGame();
    }
    public void LeaveLobby()
    {
        if(NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();

            SceneManager.LoadScene(0);
        }
    }
}
