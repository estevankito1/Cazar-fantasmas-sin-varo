
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 3f;

    private Transform player;
    private MiniGameManager gameManager;

    private bool hasAttacked = false;

    void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        gameManager =
            FindFirstObjectByType<MiniGameManager>();
    }

    void Update()
    {
        if (player == null) return;
        if (gameManager == null) return;
        if (gameManager.gameOver) return;

        Vector3 target = player.position;
        target.y = transform.position.y;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasAttacked) return;

        if (other.CompareTag("Player"))
        {
            hasAttacked = true;

            if (gameManager != null &&
                !gameManager.gameOver)
            {
                gameManager.TakeDamage();
            }

            Destroy(gameObject);
        }
    }
}



