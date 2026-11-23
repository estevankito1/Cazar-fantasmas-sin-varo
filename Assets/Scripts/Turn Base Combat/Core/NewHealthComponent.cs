using UnityEngine;

public class NewHealthComponent : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 20;
    public int currentHealth;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
        IsDead = false;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead || damage <= 0)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        Debug.Log(gameObject.name + " HP: " + currentHealth);
    }

    public void Heal(int amount)
    {
        if (IsDead || amount <= 0)
            return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log(gameObject.name + " HP: " + currentHealth);
    }

    private void Die()
    {
        IsDead = true;
        Debug.Log(gameObject.name + " ha sido derrotado.");
    }
}
