using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Tower : NetworkBehaviour
{
    [SerializeField] private Targeter targeter = null;
    public GameObject enemyTarget = null;
    public override void OnStartServer()
    {
        targeter.SetTarget(enemyTarget);
    }
    public void Update()
    {
        if(enemyTarget == null)
        {
            // Debug.Log("Target is gone");
        }
    }
}
