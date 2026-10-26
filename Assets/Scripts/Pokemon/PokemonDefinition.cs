using UnityEngine;

[CreateAssetMenu(fileName = "New Pokemon Definition", menuName = "Pokemon/Definition")]
public class PokemonDefinition : ScriptableObject
{
    public string PokemonName => m_PokemonName;
    public string Description => m_Description;
    public GameObject Model => m_Model;
    public Sprite Sprite => m_Sprite;
    public int  Health => m_Health;
    public int Attack => m_Attack;
    public int SpAttack => m_SpAttack;
    public int Defense => m_Defense;
    public int SpDefense => m_SpDefense;
    public int Speed => m_Speed;
    public PokemonTypes.TypeList MainType => m_MainType;
    public PokemonTypes.TypeList SecondaryType => m_SecondaryType;
    public int EvolutionLevel => m_EvolutionLevel;
    public PokemonDefinition Evolution => m_Evolution;
    public PokemonMove[] Moves => m_Moves;
    
    [Header("General:")]
    [SerializeField] private string m_PokemonName;
    [SerializeField,TextArea] private string m_Description;
    [SerializeField] private GameObject m_Model;
    [SerializeField] private Sprite m_Sprite;
    
    [Header("Stats:")]
    [SerializeField, Range(0, 100)] private int m_Health;
    [SerializeField, Range(0, 100)] private int m_Attack;
    [SerializeField, Range(0, 100)] private int m_Defense;
    [SerializeField, Range(0, 100)] private int m_SpAttack;
    [SerializeField, Range(0, 100)] private int m_SpDefense;
    [SerializeField, Range(0, 100)] private int m_Speed;

    [SerializeField] private PokemonMove [] m_Moves;
    
    [Header("Types:")]
    [SerializeField] private PokemonTypes.TypeList m_MainType;
    [SerializeField] private PokemonTypes.TypeList m_SecondaryType;
    
    [Header("Evolution:")]
    [SerializeField] private int m_EvolutionLevel;
    [SerializeField] private PokemonDefinition m_Evolution;

    private void OnValidate()
    {
        if (m_Moves.Length > 4)
        {
            Debug.LogError("Pokemon can only have 4 moves");
            System.Array.Resize(ref m_Moves, 4);
        }
    }

}
