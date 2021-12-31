using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerTD : NetworkBehaviour
{
    public static event Action<PlayerTD> ServerOnPlayerEnter;
    public static event Action<PlayerTD> ServerOnPlayerLeft;

    #region Server

    public override void OnStartServer()
    {
        ServerOnPlayerEnter?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnPlayerLeft?.Invoke(this);
    }
    #endregion
}
