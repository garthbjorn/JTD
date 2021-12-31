using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseHealthDisplay : MonoBehaviour
{
    [SerializeField] private GameManagerTD gameManager = null;
    [SerializeField] private TMP_Text livesValue = null;

    private void Awake()
    {
        gameManager.HandleEnemyGoal += HandleGoalReached;
    }

    private void OnDestroy()
    {
        gameManager.HandleEnemyGoal -= HandleGoalReached;
    }

    private void HandleGoalReached(int remainingLives)
    {
        
        livesValue.text = remainingLives.ToString();
    }
}
