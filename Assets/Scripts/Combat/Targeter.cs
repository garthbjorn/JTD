using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Targeter : NetworkBehaviour
{
    private Targetable target;

    public Targetable GetTarget()
    {
        return target;
    }

    public override void OnStartServer()
    {
        // GameOverHandler.ServerOnGameOver += ServerHandleGameOver;
    }

    public override void OnStopServer()
    {
        // GameOverHandler.ServerOnGameOver -= ServerHandleGameOver;
    }

    public void SetTarget(GameObject targetGameObject)
    {
        if(!targetGameObject.TryGetComponent<Targetable>(out Targetable newTarget)) { return;}
        
        target = newTarget;
    }

    #region Server
    [Server]
    public void clearTarget()
    {
        target = null;
    }

    [Server]
    private void ServerHandleGameOver()
    {
        clearTarget();
    }
    
    #endregion

    #region Client

    #endregion
}