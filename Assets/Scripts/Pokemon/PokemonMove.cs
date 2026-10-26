using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Pokemon/Move")]
public class PokemonMove : ScriptableObject
{
    public string MoveName => m_MoveName;
    public string MoveDescription => m_MoveDescription;
    public int Power => m_Power;
    public int Accuracy => m_Accuracy;
    public int PowerPoints => m_PowerPoints;
    public bool IsSpecial => m_IsSpecial;
    public PokemonTypes.TypeList MoveType => m_MoveType;
    public string AnimationName => m_AnimationName;
    public Sprite Icon => m_Icon;
    public AudioClip SoundEffect => m_SoundEffect;
    public GameObject Effect => m_VisualEffect;
    
    [Header ("General")]
    [SerializeField] private string m_MoveName;
    [SerializeField,TextArea] private string m_MoveDescription;
    
    [Header ("Stats")]
    [SerializeField] private int m_Power;
    [SerializeField,Range(0,100)] private int m_Accuracy;
    [SerializeField] private int m_PowerPoints;
    [SerializeField] private bool m_IsSpecial;
    [SerializeField] private PokemonTypes.TypeList m_MoveType;
    
    [Header ("Visuals and Effects")]
    [SerializeField] private string m_AnimationName;
    [SerializeField] private Sprite m_Icon;
    [SerializeField] private AudioClip m_SoundEffect;
    [SerializeField] private GameObject m_VisualEffect;
    
}
