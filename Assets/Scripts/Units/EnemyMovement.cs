using System;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    public event Action<GameObject> ServerOnGoalReached;
    public GameObject goal = null;


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

        if((transform.position.x == goal.transform.position.x) && (transform.position.z == goal.transform.position.z))
        {
            agent.ResetPath();
            ServerOnGoalReached?.Invoke(gameObject);
            return;
        }
        if (!agent.hasPath)
        {
            agent.SetDestination(goal.transform.position);
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
