using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 playerPosition;

    [field: SerializeField] public float Health { get; private set; } = 5;

    private float speed = 0.02f;
    private float damage = 1f;

    private Coroutine MoveToPlayerCoroutine;

    [SerializeField] private GameObject experience;

    private void Awake()
    {
        MoveToPlayerCoroutine = StartCoroutine(MoveToPlayer());
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            StopCoroutine(MoveToPlayerCoroutine);

            Instantiate(experience, transform.position, Quaternion.identity, null);

            Destroy(gameObject);
        }
    }

    private IEnumerator MoveToPlayer()
    {
        while (true)
        {
            playerPosition = Player.Instance.transform.position;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerPosition, speed /**Time.deltaTime*/);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
