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

    }
    public void Update()
    {
        if (enemyTarget != null)
        {
            targeter.SetTarget(enemyTarget);
            //enemyTarget.TryGetComponent<Health>(out Health health);
            //health.DealDamage(10);
        }
    }
}
