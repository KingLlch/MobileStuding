using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner _instance;

    [SerializeField] private Enemy Enemy;

    private float spawnDelay = 2f;
    private float spawnRadius = 10;
    private int maxEnemy = 100; 

    private Coroutine SpawnEnemyCoroutine;

    public List<Enemy> EnemyList;

    public static EnemySpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemySpawner>();
            }

            return _instance;
        }
    }

    private void Awake()
    {   
        _instance = this;
        SpawnEnemyCoroutine = StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {

        while (true && maxEnemy < EnemyList.Count)
        {
            int spawnDegree = Random.Range(0, 360);
            Vector3 spawnPoint = new Vector3(Mathf.Sin(-spawnDegree * Mathf.Deg2Rad) * spawnRadius, Mathf.Cos(-spawnDegree * Mathf.Deg2Rad) * spawnRadius, 0);
            EnemyList.Add(Instantiate(Enemy, spawnPoint, Quaternion.identity, null));
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void StopSpawn()
    {
        StopCoroutine(SpawnEnemyCoroutine);
    }
}
