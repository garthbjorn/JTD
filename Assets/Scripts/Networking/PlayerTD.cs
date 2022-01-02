using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerTD : NetworkBehaviour
{
    public static event Action<PlayerTD> ServerOnPlayerEnter;
    public static event Action<PlayerTD> ServerOnPlayerLeft;

    public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;
    public static event Action ClientOnInfoUpdated;


    [SyncVar(hook = nameof(AuthorityHandlePartyOwnerStateUpdated))]
    private bool isPartyOwner = false;

    [SyncVar(hook = nameof(ClientHandleDisplayNameUpdated))]
    private string displayName;

    public string GetDisplayName()
    {
        return displayName;
    }
    public bool GetIsPartyOwner()
    {
        return isPartyOwner;
    }

    #region Server

    public override void OnStartServer()
    {
        ServerOnPlayerEnter?.Invoke(this);
        DontDestroyOnLoad(gameObject);
    }

    public override void OnStopServer()
    {
        ServerOnPlayerLeft?.Invoke(this);
    }

    [Server]
    public void SetPartyOwner(bool state)
    {
        isPartyOwner = state;
    }
    [Server]
    public void SetDisplayName(string name)
    {
        displayName = name;
    }
    [Command]
    public void CmdStartGame()
    {
        Debug.Log("PlayerTD Start1");

        if (!isPartyOwner) { return; }
        Debug.Log("PlayerTD Start2");

        ((NetworkManagerTD)NetworkManager.singleton).StartGame();
    }
    #endregion

    #region Client

    private void AuthorityHandlePartyOwnerStateUpdated(bool oldState, bool newState)
    {
        if (!hasAuthority) { return; }

        AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);
    }
    public override void OnStartClient()
    {
        if (NetworkServer.active) { return; }
        DontDestroyOnLoad(gameObject);
        ((NetworkManagerTD)NetworkManager.singleton).players.Add(this);
    }

    public override void OnStopClient()
    {
        ClientOnInfoUpdated?.Invoke();

        if (!isClientOnly) { return; }
        ((NetworkManagerTD)NetworkManager.singleton).players.Remove(this);
        if (!hasAuthority) { return; }

    }
    private void ClientHandleDisplayNameUpdated(string oldDisplayeName, string newDisplayName)
    {
        ClientOnInfoUpdated?.Invoke();
    }
    #endregion
}
