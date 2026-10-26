using TMPro;
using UnityEngine;

public class NewMoveButton : MonoBehaviour
{
    PokemonMove m_Move;

    [SerializeField] private TextMeshProUGUI m_MoveName, m_Type, m_Power;

    public void Initialize(PokemonMove move)
    {
        m_Move = move;
        m_MoveName.text = m_Move.MoveName;
        m_Type.text = $"Poder: {m_Move.Power.ToString()}";
        m_Power.text = $"Tipo: {m_Move.MoveType.ToString()}";
    }

    public void InformCombatManager()
    {
        if (CombatManager.Instance == null) return;
        CombatManager.SetPlayerMove(m_Move);
    }
}
