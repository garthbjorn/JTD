using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health health = null;
    [SerializeField] private GameObject heathBarParent = null;
    [SerializeField] private Image healthBarImage = null;
    
    private void Awake()
    {
        health.ClientOnHealthUpdated += HandleHeathUpdated;
    }

    private void OnDestroy()
    {
        health.ClientOnHealthUpdated -= HandleHeathUpdated;
    }

    private void HandleHeathUpdated(int currentHealth, int maxHealth)
    {
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }

    private void OnMouseEnter()
    {
        heathBarParent.SetActive(true);
    }

    private void OnMouseExit()
    {
        heathBarParent.SetActive(false);
    }
}
