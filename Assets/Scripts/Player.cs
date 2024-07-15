using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private static Player _instance;

    [field: SerializeField] public float MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float Health { get; private set; } = 100;

    [field: SerializeField] public float Regeneration { get; private set; } = 1;
    [field: SerializeField] public float RegenerationInterval { get; private set; } = 10;

    [field: SerializeField] public float MaxExperience { get; private set; } = 100;
    [field: SerializeField] public float Experience { get; private set; } = 99;
    [field: SerializeField] public float Level { get; private set; } = 0;

    private Coroutine _takeDamageIntervalCoroutine;
    private Coroutine _regenerationCoroutine;

    private bool _takeDamageInterval = true;

    [HideInInspector] public UnityEvent<float, float> ChangeHealth;
    [HideInInspector] public UnityEvent<float, float> ChangeExperience;
    [HideInInspector] public UnityEvent<float> ChangeLevel;

    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Player>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        _takeDamageIntervalCoroutine = StartCoroutine(TakeDamageInterval());
        _regenerationCoroutine = StartCoroutine(RegenerationCoroutine());
    }

    private void Start()
    {
        ChangeHealth.Invoke(Health, MaxHealth);
        ChangeExperience.Invoke(Experience, MaxExperience);
        ChangeLevel.Invoke(Level);
    }

    public void TakeExperience(float expValue)
    {
        Experience += expValue;

        if (Experience == MaxExperience)
        {
            ChangeLevel.Invoke(Level);
            Experience = 0;
            MaxExperience += 10;
        }

        ChangeExperience.Invoke(Experience, MaxExperience);
    }

    public void TakeDamage(float damage)
    {
        if (_takeDamageInterval)
        {
            Health -= damage;

            if (Health <= 0)
            {
                //gameover
            }

            ChangeHealth.Invoke(Health, MaxHealth);
            _takeDamageInterval = false;
        }
    }

    public void TakeHealth(float health)
    {
        if (_takeDamageInterval)
        {
            Health += health;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            ChangeHealth.Invoke(Health, MaxHealth);
        }
    }

    private IEnumerator TakeDamageInterval()
    {
        while (true)
        {
            if (!_takeDamageInterval)
                _takeDamageInterval = true;

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator RegenerationCoroutine()
    {
        while (true)
        {
            TakeHealth(Regeneration);
            yield return new WaitForSeconds(RegenerationInterval);
        }
    }

    public void ChangeModifiers(float maxHealth, float regeneration, float regenerationInterval)
    {
        MaxHealth += maxHealth;
        Regeneration += regeneration;
        RegenerationInterval -= regenerationInterval;
    }
}
