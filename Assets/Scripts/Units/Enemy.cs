using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : NetworkBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement = null;
    [SerializeField] private Health health = null;
    [SerializeField] private Targeter targeter = null;


    public EnemyMovement GetEnemyMovement()
    {
        return enemyMovement;
    }

    public Targeter GetTargeter()
    {
        return targeter;
    }

    #region Server

    public override void OnStartServer()
    {
        health.ServerOnDie += ServerHandleDie;
        enemyMovement.ServerOnGoalReached += ServerHandleGoalReached;
    }

    public override void OnStopServer()
    {
        health.ServerOnDie -= ServerHandleDie;
        enemyMovement.ServerOnGoalReached -= ServerHandleGoalReached;
    }

    [Server]
    private void ServerHandleGoalReached(GameObject gameObject)
    {
        NetworkServer.Destroy(gameObject);
    }
    [Server]
    private void ServerHandleDie()
    {
        NetworkServer.Destroy(gameObject);
    }
    
    #endregion
}
