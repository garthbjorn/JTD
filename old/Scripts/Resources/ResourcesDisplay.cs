using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class ResourcesDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text resourcesText = null;

    private PlayerTD player;

    private void Update()
    {
        if(player == null)
        {
            if(NetworkClient.connection != null)
            {
                if(NetworkClient.connection.identity != null)
                {
                    player = NetworkClient.connection.identity.GetComponent<PlayerTD>();

                    if(player != null)
                    {
                        ClientHandleResourcesUpdated(player.GetResources());
                        player.ClientOnResourcesUpdated += ClientHandleResourcesUpdated;
                    }
                }
            }
            
        }
    }

    private void OnDestroy()
    {
        player.ClientOnResourcesUpdated -= ClientHandleResourcesUpdated;
    }

    private void ClientHandleResourcesUpdated(int resources)
    {
        resourcesText.text = $"Resources: {resources}";
    }
}
