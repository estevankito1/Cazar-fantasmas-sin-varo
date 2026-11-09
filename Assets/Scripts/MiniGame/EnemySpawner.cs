
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject enemyPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnDistance = 12f;
    public float spawnInterval = 2f;
    public float spawnHeightOffset = 0.5f;

    private float timer;

    void Update()
    {
        if (enemyPrefab == null || player == null)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector3[] directions =
        {
            Vector3.forward,
            Vector3.back,
            Vector3.right,
            Vector3.left
        };

        // Elegir dirección aleatoria
        Vector3 direction = directions[
            Random.Range(0, directions.Length)
        ];

        // Calcular posición de aparición
        Vector3 spawnPosition =
            player.position + direction * spawnDistance;

        // Ajustar altura
        spawnPosition.y =
            player.position.y + spawnHeightOffset;

        // Crear enemigo
        Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}
