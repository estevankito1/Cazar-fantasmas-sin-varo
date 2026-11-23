using UnityEngine;

public class NewCombatManager : MonoBehaviour
{
    public enum CombatState
    {
        PlayerTurn,
        EnemyTurn,
        Victory,
        Defeat
    }

    [Header("Combat Settings")]
    public CombatState currentState;

    [Header("Characters")]
    public NewHealthComponent player;
    public NewHealthComponent enemy;

    private void Start()
    {
        currentState = CombatState.PlayerTurn;

        Debug.Log("¡Comienza el combate!");
        Debug.Log("Turno del jugador.");
    }

    public void EndPlayerTurn()
    {
        if (currentState != CombatState.PlayerTurn)
            return;

        currentState = CombatState.EnemyTurn;

        Debug.Log("Turno del enemigo.");
        EnemyTurn();
    }

    private void EnemyTurn()
    {
        if (enemy == null || player == null)
            return;

        if (enemy.IsDead || player.IsDead)
            return;

        player.TakeDamage(2);

        if (player.IsDead)
        {
            currentState = CombatState.Defeat;
            Debug.Log("¡Has sido derrotado!");
            return;
        }

        currentState = CombatState.PlayerTurn;

        Debug.Log("Turno del jugador.");
    }
}
