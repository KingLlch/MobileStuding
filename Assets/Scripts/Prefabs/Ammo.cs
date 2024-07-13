using UnityEngine;

public class Ammo : MonoBehaviour
{
    private float speed = 10f;
    private int penetration = 1;
    private float damage = 1;

    private float direction;

    private Vector2 startPosition;
    private Vector3 targetPosition;

    private float maxRange = 10;

    public void Initialize(float speedModifier, float damageModifier, int penetrationModifier, float _direction)
    {
        speed += speedModifier;
        damage += damageModifier;
        penetration += penetrationModifier;
        direction = _direction;

        startPosition = transform.position;
        targetPosition = new Vector3(Mathf.Sin(-direction * Mathf.Deg2Rad) * maxRange, Mathf.Cos(-direction * Mathf.Deg2Rad) * maxRange, 0);
    }

    private void FixedUpdate()
    {
        transform.position += targetPosition.normalized * speed * Time.deltaTime;

        if (Vector2.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            penetration--;

            if (penetration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
