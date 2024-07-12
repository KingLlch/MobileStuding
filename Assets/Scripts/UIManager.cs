using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private Image HealthImage;

    private void Awake()
    {
        Player.Instance.ChangeHealth.AddListener(ChangeHealth);
    }

    public void ChangeHealth(float health, float maxHealth)
    {
        HealthImage.fillAmount = health/maxHealth;
        Health.text = health + "/" + maxHealth;
    }
}
