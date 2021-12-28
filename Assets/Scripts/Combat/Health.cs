using System;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar(hook = nameof(HandleHeathUpdated))]
    private int currentHealth;

    public event Action ServerOnDie;

    public event Action<int, int> ClientOnHealthUpdated;
    #region Server

    public override void OnStartServer()
    {
        currentHealth = maxHealth;
        //UnitBase.ServerOnPlayerDie += ServerHandlePlayerDie;
    }

    public override void OnStopServer()
    {
        //UnitBase.ServerOnPlayerDie -= ServerHandlePlayerDie;
    }

    private void ServerHandlePlayerDie(int connectionID)
    {
        if(connectionToClient.connectionId != connectionID) {return;}

        DealDamage(currentHealth);
    }

    [Server]
    public void DealDamage(int damageAmount)
    {
        if(currentHealth == 0) {return;}

        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
        
        if(currentHealth != 0) {return;}

        ServerOnDie?.Invoke();
    }
    #endregion

    #region Client

    private void HandleHeathUpdated(int oldHealth, int newHealth)
    {
        ClientOnHealthUpdated?.Invoke(newHealth, maxHealth);
    }
    #endregion
}
