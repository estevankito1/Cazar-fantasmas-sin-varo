using System.Collections.Generic;
using UnityEngine;

public class PokemonInformation
{
    public PokemonInformation(PokemonDefinition definition)
    {
        m_Definition = definition;
        m_Name = definition.PokemonName;
        m_CurrentHealth = maxHealth;
        m_Moves = new List<PokemonMove>(4);
        foreach (PokemonMove move in definition.Moves)
        {
            if  (move == null)
            {
                continue;
            }
            m_Moves.Add(move);
        }
    }
    
    public int currentHealth => m_CurrentHealth;
    public string Name => m_Name;
    public int maxHealth => m_Definition.Health + 2 * m_Level;
    public int Attack => m_Definition.Attack + m_Level;
    public int SpAttack => m_Definition.SpAttack + m_Level;
    public int Defense => m_Definition.Defense + m_Level;
    public int SpDefense => m_Definition.SpDefense + m_Level;
    public int Speed => m_Definition.Speed + m_Level;
    
    public PokemonTypes.TypeList mainType => m_Definition.MainType;
    public PokemonTypes.TypeList secondaryType => m_Definition.SecondaryType;
    
    public PokemonMove[] Moves => m_Moves.ToArray();

    public PokemonDefinition m_Definition;
    private string m_Name;
    private int m_Level;
    private int m_Xp;
    private int m_CurrentHealth;

    private List<PokemonMove> m_Moves;

    public void GetDamage(int damage)
    {
        m_CurrentHealth -= damage;
        if (m_CurrentHealth < 0)
            m_CurrentHealth = 0;
    }

    public void Heal(int amount)
    {
        m_CurrentHealth += amount;
        if (m_CurrentHealth > maxHealth)
            m_CurrentHealth = maxHealth;
    }

    public void GainXp(int amount)
    {
        m_Xp += amount;
        if(m_Xp >= GetXpForNextLevel())
        {
            m_Xp -= GetXpForNextLevel();
            LevelUp();
        }
    }
    
    private int GetXpForNextLevel()
    {
        return (int)(Mathf.Pow(m_Level + 1, 1.25F) * 100);
    }

    private void LevelUp()
    {
        m_Level++;
        m_CurrentHealth = maxHealth;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrEmpty(newName))
        {
            Debug.LogError("Pokemon name is null or empty");
            return;
        }
        m_Name = newName;
    }

    public void LearnMove(PokemonMove NewMove)
    {
        if (m_Moves.Count >= 4)
        {
            Debug.LogWarning("Cannot learn more than 4 moves. Please forget an existing move first.");
            return;
        }
        m_Moves.Add(NewMove);
    }

    public void ForgetMove(PokemonMove moveToForget)
    {
        if (m_Moves.Contains(moveToForget))
        {
            m_Moves.Remove(moveToForget);
        }
        else
        {
            Debug.LogWarning("Cannot forget a move that is not learned.");
        }
    }
    public GameObject SpawnModel(Transform p_Parent)
    {
        if (m_Definition.Model == null)
        {
            Debug.LogError("Pokemon model is null");
            return null;
        }
        GameObject t_Model = UnityEngine.Object.Instantiate(m_Definition.Model, p_Parent);
        t_Model.transform.localPosition = Vector3.zero;
        t_Model.transform.localRotation = Quaternion.identity;
        t_Model.transform.localScale = Vector3.one;
        t_Model.name = "Model";
        return t_Model;
    }
}
