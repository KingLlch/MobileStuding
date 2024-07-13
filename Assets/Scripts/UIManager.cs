using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private Image HealthImage;
    [SerializeField] private Image ExperienceImage;
    [SerializeField] private TextMeshProUGUI Level;

    private void Awake()
    {
        Player.Instance.ChangeHealth.AddListener(ChangeHealth);
        Player.Instance.ChangeExperience.AddListener(ChangeExperience);
        Player.Instance.ChangeLevel.AddListener(ChangeLevel);
    }

    public void ChangeHealth(float health, float maxHealth)
    {
        HealthImage.fillAmount = health / maxHealth;
        Health.text = health + "/" + maxHealth;
    }

    public void ChangeExperience(float experience, float maxExperience)
    {
        ExperienceImage.fillAmount = experience / maxExperience;
    }

    public void ChangeLevel(float level)
    {
        Level.text = level.ToString();
    }
}
