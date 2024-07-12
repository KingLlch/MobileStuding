using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private static Player _instance;

    [field: SerializeField] public float MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float Health { get; private set; } = 100;

    private Coroutine _takeDamageIntervalCoroutine;
    private bool _takeDamageInterval;

    [HideInInspector] public UnityEvent<float,float> ChangeHealth;

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

        ChangeHealth.Invoke(Health, MaxHealth);
        _takeDamageIntervalCoroutine = StartCoroutine(TakeDamageInterval());
    }

    public void TakeDamage(float damage)
    {
        if (_takeDamageInterval)
        {
            Health -= damage;

            if (Health <= 0)
            {

            }

            ChangeHealth.Invoke(Health, MaxHealth);
            _takeDamageInterval = false;
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
}
