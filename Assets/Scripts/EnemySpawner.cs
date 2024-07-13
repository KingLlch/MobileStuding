using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float SpawnDelay = 2f;
    private float spawnRadius = 10;

    [SerializeField] private Enemy Enemy;
    private Coroutine SpawnEnemyCoroutine;

    private void Awake()
    {
        SpawnEnemyCoroutine = StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {

        while (true)
        {
            int spawnDegree = Random.Range(0, 360);
            Vector3 spawnPoint = new Vector3(Mathf.Sin(-spawnDegree * Mathf.Deg2Rad) * spawnRadius, Mathf.Cos(-spawnDegree * Mathf.Deg2Rad) * spawnRadius, 0);
            Instantiate(Enemy, spawnPoint, Quaternion.identity, null);
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    public void StopSpawn()
    {
        StopCoroutine(SpawnEnemyCoroutine);
    }
}
