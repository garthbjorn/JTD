using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameManagerTD : NetworkBehaviour
{
    //public static event Action ServerOnGameOver;
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private GameObject enemySpawnLocation = null;
    [SerializeField] private GameObject enemyTarget = null;
    public static event Action<string> ClientOnGameOver;

    // private List<UnitBase> bases = new List<UnitBase>();

    public void Update()
    {
        // GameObject enemyInstance = Instantiate(
        //     enemyPrefab,
        //     enemySpawnLocation.transform.position,
        //     enemySpawnLocation.transform.rotation);

        // enemyInstance.GetComponent<Enemy>().enemyTarget = enemyTarget;

        // NetworkServer.Spawn(enemyInstance);
    }

    #region Server

    public override void OnStartServer()
    {
        GameObject enemyInstance = Instantiate(
            enemyPrefab,
            enemySpawnLocation.transform.position,
            enemySpawnLocation.transform.rotation);

        enemyInstance.GetComponent<Enemy>().GetComponent<UnitMovement>().enemyTarget = enemyTarget;

        NetworkServer.Spawn(enemyInstance);
        // UnitBase.ServerOnBaseSpawned += ServerHandleBaseSpawned;
        // UnitBase.ServerOnBaseDespawned += ServerHandleBaseDespawned;

    }

    public override void OnStopServer()
    {
        // UnitBase.ServerOnBaseSpawned -= ServerHandleBaseSpawned;
        // UnitBase.ServerOnBaseDespawned -= ServerHandleBaseDespawned;
    }

    [Server]
    // private void ServerHandleBaseSpawned(UnitBase unitBase)
    // {
    //     bases.Add(unitBase);
    // }

    // [Server]
    // private void ServerHandleBaseDespawned(UnitBase unitBase)
    // {
    //     bases.Remove(unitBase);

    //     if(bases.Count != 1) {return;}

    //     int playerID = bases[0].connectionToClient.connectionId;

    //     RpcGameOver($"Player {playerID}");

    //     ServerOnGameOver?.Invoke();
    // }

    #endregion

    #region Client

    [ClientRpc]
    private void RpcGameOver(string winner)
    {
        ClientOnGameOver?.Invoke(winner);
    }

    #endregion
}
