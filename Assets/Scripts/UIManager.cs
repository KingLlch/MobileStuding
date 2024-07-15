using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private Image HealthImage;
    [SerializeField] private Image ExperienceImage;
    [SerializeField] private TextMeshProUGUI Level;

    [SerializeField] private GameObject LevelUpPanel;
    [SerializeField] private TextMeshProUGUI[] BoostsName;

    private void Awake()
    {
        Player.Instance.ChangeHealth.AddListener(ChangeHealth);
        Player.Instance.ChangeExperience.AddListener(ChangeExperience);
        Player.Instance.ChangeLevel.AddListener(ChangeLevel);

        ChooseBoost.Instance.StartChooseBoost.AddListener(BoostIcons);
        ChooseBoost.Instance.EndChooseBoost.AddListener(HideBoostIcons);
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

    private void BoostIcons(int first, int second, int third)
    {
        BoostsName[0].text = ((BoostEnum)first).ToString();
        BoostsName[1].text = ((BoostEnum)second).ToString();
        BoostsName[2].text = ((BoostEnum)third).ToString();

        LevelUpPanel.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    private void HideBoostIcons()
    {
        LevelUpPanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
