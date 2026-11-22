using UnityEngine;
using UnityEngine.AI;

public class ePool : MonoBehaviour
{
    [Header("Pool Settings")]
    public GameObject enemyPrefab;
    public int poolSize = 20;
    public int maxActiveEnemies = 5; // Maximum enemies allowed alive at once
    public float spawnInterval = 3f; // Seconds between spawns

    [Header("Spawn Area")]
    public Vector3 spawnCenter = Vector3.zero;
    public float spawnRadius = 10f;

    private GameObject[] enemyPool;
    private float nextSpawnTime;

    public Transform personaje; 
    public Transform[] patrolPoints; 
    public KnockoutCounter knockoutCounter; 

    void Start()
    {
        // Pre-instantiate the pool
        enemyPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            enemyPool[i] = Instantiate(enemyPrefab, transform);
            enemyPool[i].SetActive(false);
        }
    }

    void Update()
    {
        // Check if enough time has passed AND we haven't hit the active enemy cap
        if (Time.time >= nextSpawnTime && GetActiveEnemyCount() < maxActiveEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    // Counts how many pooled enemies are currently active in the scene
    public int GetActiveEnemyCount()
    {
        int count = 0;
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if (enemyPool[i].activeSelf)
            {
                count++;
            }
        }
        return count;
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if (!enemyPool[i].activeSelf)
            {
                // Pick a random patrol point as the spawn target if available
                Vector3 targetPos = spawnCenter + Random.insideUnitSphere * spawnRadius;
                if (patrolPoints != null && patrolPoints.Length > 0)
                {
                    Transform chosenPoint = patrolPoints[Random.Range(0, patrolPoints.Length)];
                    if (chosenPoint != null)
                    {
                        targetPos = chosenPoint.position;
                    }
                }

                if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 3f, NavMesh.AllAreas))
                {
                    GameObject enemy = enemyPool[i];

                    if (enemy.TryGetComponent<Rigidbody>(out Rigidbody rb))
                    {
                        rb.linearVelocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                    }

                    enemy.SetActive(true);

                    if (enemy.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
                    {
                        agent.Warp(hit.position);
                        agent.enabled = true;
                        agent.isStopped = false;
                    }
                    else
                    {
                        enemy.transform.position = hit.position;
                    }
                }
                return;
            }
        }
    }
}
