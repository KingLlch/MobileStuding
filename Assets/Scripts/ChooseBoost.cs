using UnityEngine;
using UnityEngine.Events;

public class ChooseBoost : MonoBehaviour
{
    private static ChooseBoost _instance;

    private float _maxHealth = 10;
    private float _regeneration = 2;
    private float _regenerationInterval = 1;

    private float _shootDelay = 0.05f;
    private float _speed = 1;
    private float _damage = 1;
    private int _penetration = 1;

    private int[] randomBoost = { 0, 0, 0 };

    [HideInInspector] public UnityEvent<int, int, int> StartChooseBoost;
    [HideInInspector] public UnityEvent EndChooseBoost;

    public static ChooseBoost Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ChooseBoost>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        Player.Instance.ChangeLevel.AddListener(ChangeLevel);
    }

    private void ChangeLevel(float level)
    {
        for (int i = 0; i < 3; i++)
        {
            randomBoost[i] = UnityEngine.Random.Range(0, 7);
        }

        StartChooseBoost.Invoke(randomBoost[0], randomBoost[1], randomBoost[2]);
    }

    public void ChooseBoostInUI(int number)
    {
        switch (randomBoost[number])
        {
            case 0: Player.Instance.ChangeModifiers(_maxHealth, 0, 0); Player.Instance.TakeHealth(_maxHealth); break;
            case 1: Player.Instance.ChangeModifiers(0, _regeneration, 0); break;
            case 2: Player.Instance.ChangeModifiers(0, 0, _regenerationInterval); break;
            case 3: AmmoFactory.Instance.ChangeAmmoModifiers(_shootDelay, 0, 0, 0); break;
            case 4: AmmoFactory.Instance.ChangeAmmoModifiers(0, _speed, 0, 0); break;
            case 5: AmmoFactory.Instance.ChangeAmmoModifiers(0, 0, _damage, 0); break;
            case 6: AmmoFactory.Instance.ChangeAmmoModifiers(0, 0, 0, _penetration); break;
        }

        EndChooseBoost.Invoke();
    }
}

public enum BoostEnum
{
    ћаксимальное«доровье = 0,
    –егенераци€ = 1,
    »нтервал–егенерации = 2,
    »нтервал¬ыстрела = 3,
    —коростьѕули = 4,
    ”ронѕули = 5,
    ѕробиваниеѕули = 6
}
