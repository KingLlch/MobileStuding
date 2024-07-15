using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public bool IsPause = false; 

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsPause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsPause = false;
    }
}
