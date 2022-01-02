using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = new LayerMask();

    private Camera mainCamera;
    private PlayerTD player;
    private Vector2 startposition;
    public List<Enemy> SelectedEnemys { get; } = new List<Enemy>();

    private void Start()
    {
        mainCamera = Camera.main;
        // Unit.AuthorityOnUnitDespawned += AuthorityHandleUnitDespawned;
        // GameOverHandler.ClientOnGameOver += ClientHandleGameOver;
    }

    private void OnDestroy()
    {
        // Unit.AuthorityOnUnitDespawned -= AuthorityHandleUnitDespawned;
        // GameOverHandler.ClientOnGameOver -= ClientHandleGameOver;
    }

    private void Update()
    {
        if (player == null)
        {
            if (NetworkClient.connection != null)
            {
                if (NetworkClient.connection.identity != null)
                {
                    player = NetworkClient.connection.identity.GetComponent<PlayerTD>();
                }
            }

        }
        else
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                foreach (Enemy selectedEnemy in SelectedEnemys)
                {
                    selectedEnemy.Deselect();
                }
                SelectedEnemys.Clear();
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                ClearSelectionArea();
            }
            else if (Mouse.current.leftButton.isPressed)
            {
                // UpdateSelectionArea();
            }
        }
    }

    private void ClearSelectionArea()
    {

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }

        if (!hit.collider.TryGetComponent<Enemy>(out Enemy enemy)) { return; }

        SelectedEnemys.Add(enemy);

        foreach (Enemy selectedEnemy in SelectedEnemys)
        {
            selectedEnemy.Select();
        }
        return;
    }

    // private void AuthorityHandleUnitDespawned(Unit unit)
    // {
    //     SelectedUnits.Remove(unit);
    // }

    // private void ClientHandleGameOver(string winner)
    // {
    //     enabled = false;
    // }
}
