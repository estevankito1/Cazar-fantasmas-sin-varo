using UnityEngine;

public class WaitForActionState : State
{
    public override void Enter()
    {
        Debug.Log("Esperando input");
        CombatManager.Instance.m_UI.SetTextBox($"Tu {CombatManager.Instance.playerPokemon.name} te está esperando...");
    }

    public override void FixedUpdate()
    {
        if (CombatManager.Instance.playerMove != null)
        {
            CombatManager.Instance.BuildTurnQueue();
            CombatManager.PlayNextTurn();
        }
    }

    public override void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Input recibido");
            //CombatManager.Instance.playerMove = CombatManager.Instance.playerPokemon.UseRandomMove();
        }
    }

    public override void Exit()
    {
    }
}