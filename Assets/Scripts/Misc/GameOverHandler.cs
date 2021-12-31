using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameOverHandler : NetworkBehaviour
{

    private List<PlayerTD> players = new List<PlayerTD>();

    #region Server

    public override void OnStartServer()
    {
        PlayerTD.ServerOnPlayerEnter += ServerHandlePlayerEnter;
        PlayerTD.ServerOnPlayerLeft += ServerHandlePlayerLeft;
    }

    public override void OnStopServer()
    {
        PlayerTD.ServerOnPlayerEnter -= ServerHandlePlayerEnter;
        PlayerTD.ServerOnPlayerLeft -= ServerHandlePlayerLeft;
    }

    [Server]
    private void ServerHandlePlayerEnter(PlayerTD player)
    {
        players.Add(player);
    }

    [Server]
    private void ServerHandlePlayerLeft(PlayerTD player)
    {
        players.Remove(player);
    }

    #endregion
}
