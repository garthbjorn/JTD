﻿using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerRTS : NetworkManager
{
    [SerializeField] private GameObject enemyPrefab = null;
    // [SerializeField] private GameOverHandler gameOverHandlerPrefab = null;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        GameObject enemyInstance = Instantiate(
            enemyPrefab, 
            conn.identity.transform.position, 
            conn.identity.transform.rotation);

            NetworkServer.Spawn(enemyInstance, conn);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if(SceneManager.GetActiveScene().name.StartsWith("Scene_Map"))
        {
            // GameOverHandler gameOverHandlerInstance = Instantiate(gameOverHandlerPrefab);

            // NetworkServer.Spawn(gameOverHandlerInstance.gameObject);

        }
    }
}
