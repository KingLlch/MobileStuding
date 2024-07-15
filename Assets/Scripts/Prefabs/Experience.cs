using UnityEngine;

public class Experience : MonoBehaviour
{
    private float expValue = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeExperience(expValue);
            Destroy(gameObject);
        }
    }
}
