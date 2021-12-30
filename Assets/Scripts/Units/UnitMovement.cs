using System;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    public event Action ServerOnDie;
    public GameObject enemyTarget = null;


    #region Server

    public override void OnStartServer()
    {
        // GameOverHandler.ServerOnGameOver += ServerHandleGameOver;
    }

    public override void OnStopServer()
    {
        // GameOverHandler.ServerOnGameOver -= ServerHandleGameOver;
    }

    [ServerCallback]
    private void Update()
    {

        if((transform.position.x == enemyTarget.transform.position.x) && (transform.position.z == enemyTarget.transform.position.z))
        {
            agent.ResetPath();
            gameObject.GetComponent<Enemy>().endReached = true;
            ServerOnDie?.Invoke();
            return;
        }
        if (!agent.hasPath)
        {
            agent.SetDestination(enemyTarget.transform.position);
        }
        else
        {
            return;
        }
    }

    [Server]
    private void ServerHandleGameOver()
    {
        agent.ResetPath();
    }

    #endregion
}
