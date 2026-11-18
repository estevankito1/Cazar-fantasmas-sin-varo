
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Vidas: " + currentHealth);
    }

    public void TakeDamage()
    {
        currentHealth--;

        Debug.Log("Vidas restantes: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡GAME OVER!");
        Time.timeScale = 0f;
    }
}
