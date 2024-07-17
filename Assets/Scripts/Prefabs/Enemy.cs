using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Vector3 playerPosition;

    [field: SerializeField] public float Health { get; private set; } = 5;

    private float speed;
    private float damage = 1f;

    private Coroutine MoveToPlayerCoroutine;

    [SerializeField] private GameObject experience;

    private void Awake()
    {
        MoveToPlayerCoroutine = StartCoroutine(MoveToPlayer());
        speed = Random.Range(0.01f, 0.04f);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            StopCoroutine(MoveToPlayerCoroutine);

            Instantiate(experience, transform.position, Quaternion.identity, null);

            EnemySpawner.Instance.EnemyList.Remove(this);

            Destroy(gameObject);
        }
    }

    private IEnumerator MoveToPlayer()
    {
        while (true)
        {
            playerPosition = Player.Instance.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed /**Time.deltaTime*/);

            Vector3 rotate = playerPosition - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(rotate.x, rotate.y) * Mathf.Rad2Deg);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>().TakeDamageIntervalBool)
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
