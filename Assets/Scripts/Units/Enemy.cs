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
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;


    public EnemyMovement GetEnemyMovement()
    {
        return enemyMovement;
    }

    public Targeter GetTargeter()
    {
        return targeter;
    }
    #region Client

    [Client]
    public void Select()
    {
        onSelected?.Invoke();
    }
    [Client]
    public void Deselect()
    {
        onDeselected?.Invoke();
    }
    #endregion

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
    private void ServerHandleDie(GameObject gameObject)
    {
        NetworkServer.Destroy(gameObject);
    }

    #endregion
}
