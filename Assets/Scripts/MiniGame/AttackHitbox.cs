using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private MiniGameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<MiniGameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (gameManager != null && !gameManager.gameOver)
            {
                gameManager.RegisterKill();
                Destroy(other.gameObject);
            }
        }
    }
}
