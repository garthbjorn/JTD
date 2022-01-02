using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameManagerTD : NetworkBehaviour
{
    //public static event Action ServerOnGameOver;
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private GameObject enemySpawnLocation = null;
    [SerializeField] private GameObject goal = null; // The base
    [SerializeField] private List<GameObject> towersList = null;

    public static event Action<string> ClientOnGameOver;
    public event Action<int> HandleEnemyGoal;

    private List<GameObject> currentEnemies = new List<GameObject> { };

    // private List<UnitBase> bases = new List<UnitBase>();
    private int remainingLives = 20;

    public void Update()
    {

    }

    #region Server

    public override void OnStartServer()
    {

    }

    private void spawnEnemy()
    {
        GameObject enemyInstance = Instantiate(
            enemyPrefab,
            enemySpawnLocation.transform.position,
            enemySpawnLocation.transform.rotation);

        // Target the base
        enemyInstance.GetComponent<Enemy>().GetComponent<EnemyMovement>().goal = goal;

        NetworkServer.Spawn(enemyInstance);
        towersList[0].GetComponent<Tower>().enemyTarget = enemyInstance;

        enemyInstance.GetComponent<Enemy>().GetComponent<EnemyMovement>().ServerOnGoalReached += ServerHandleGoalReached;
        enemyInstance.GetComponent<Enemy>().GetComponent<Health>().ServerOnDie += ServerHandleDie;
        currentEnemies.Add(enemyInstance);
    }

    [Server]
    private void ServerHandleGoalReached(GameObject gameObject)
    {
        remainingLives--;

        HandleEnemyGoal?.Invoke(remainingLives);
        currentEnemies.Remove(gameObject);

        if(remainingLives == 0)
        {
            // Game Over
        }
    }

    [Server]
    private void ServerHandleDie(GameObject gameObject)
    {
        currentEnemies.Remove(gameObject);
    }
    public override void OnStopServer()
    {

    }

    #endregion

    #region Client

    [ClientRpc]
    private void RpcGameOver(string winner)
    {
        ClientOnGameOver?.Invoke(winner);
    }

    #endregion
}
