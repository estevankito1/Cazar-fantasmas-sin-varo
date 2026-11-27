using System;
using UnityEngine;


[Serializable, Flags]
public enum PlayerAction
{
    None = 0,
    AguaBendita = 1,
    Cruz = 2,
    Rosario = 3,
    Talisman = 4
}

public class NewCombatManager : MonoBehaviour
{
    public enum CombatState
    {
        WaitingForDoll,
        // player turn states

        WaitForAction,
        PlayerAction,
        EnemyReact,
        CheckWin,
        // enemy turn states

        EnemyAction,
        PlayerReact,
        CheckLose,
        // is win or lose

        Win,
        Lose

    }

    [Header("Combat Settings")]
    public CombatState currentState;

    [Header("Characters")]
    public NewHealthComponent player;
    public NewHealthComponent enemy;

    private PlayerAction playerAction;

    public void StartCombat()
    {
        if (currentState != CombatState.WaitingForDoll)
            return;

        ChangeState(CombatState.WaitForAction);

        Debug.Log("¡La muñeca ha desbloqueado el combate!");
        Debug.Log("¡Comienza el combate!");
        Debug.Log("Turno del jugador.");
    }

    private void Start()
    {
        currentState = CombatState.WaitingForDoll;

        Debug.Log("Esperando a que encuentres la muñeca...");
    }

    public void Update()
    {
        switch (currentState)
        {
            case CombatState.WaitingForDoll:

                return;
            case CombatState.WaitForAction:
                if (playerAction is PlayerAction.None) return;
                ChangeState(CombatState.PlayerAction);
                return;
            case CombatState.PlayerAction:
                switch (playerAction)
                {
                    case PlayerAction.AguaBendita:
                        return;
                    case PlayerAction.Cruz:
                        return;
                    case PlayerAction.Rosario:
                        return;
                    case PlayerAction.Talisman:
                        return;
                }
                return;
            case CombatState.EnemyReact:

                return;
            case CombatState.CheckWin:
                return;
            case CombatState.EnemyAction:
                return;
            case CombatState.PlayerReact:
                return;
            case CombatState.CheckLose:
                return;
            case CombatState.Win:
                return;
            case CombatState.Lose:
                return;
        }
    }

    public void ChoseAction(PlayerAction Action) => playerAction = Action;

    public void ChangeState(CombatState NextState)
    {
        currentState = NextState;

        switch (currentState)
        {
            case CombatState.WaitingForDoll:
                return;
            case CombatState.WaitForAction:
                return;
            case CombatState.PlayerAction:
                return;
            case CombatState.EnemyReact:
                playerAction = PlayerAction.None;
                return;
            case CombatState.CheckWin:
                return;
            case CombatState.EnemyAction:
                return;
            case CombatState.PlayerReact:
                return;
            case CombatState.CheckLose:
                return;
            case CombatState.Win:
                return;
            case CombatState.Lose:
                return;
        }
    }
}
