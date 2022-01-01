using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerTD : NetworkBehaviour
{
    public static event Action<PlayerTD> ServerOnPlayerEnter;
    public static event Action<PlayerTD> ServerOnPlayerLeft;

    public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;

    [SyncVar(hook = nameof(AuthorityHandlePartyOwnerStateUpdated))]
    private bool isPartyOwner = false;

    public bool GetIsPartyOwner()
    {
        return isPartyOwner;
    }

    #region Server

    public override void OnStartServer()
    {
        ServerOnPlayerEnter?.Invoke(this);
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
    [Command]
    public void CmdStartGame()
    {
        if(!isPartyOwner) {return;}

        ((NetworkManagerTD)NetworkManager.singleton).StartGame();
    }
    #endregion

    #region Client

    private void AuthorityHandlePartyOwnerStateUpdated(bool oldState, bool newState)
    {
        if(!hasAuthority) {return;}

        AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);
    }
    public override void OnStartClient()
    {
        if (NetworkServer.active) { return; }
        ((NetworkManagerTD)NetworkManager.singleton).players.Add(this);
    }

    public override void OnStopClient()
    {
        if (!isClientOnly) { return; }
        ((NetworkManagerTD)NetworkManager.singleton).players.Remove(this);
        if (!hasAuthority) { return; }

    }
    #endregion
}
