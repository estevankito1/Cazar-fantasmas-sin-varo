using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static NewCombatUI NewCombatUI => Instantiate(Instance.m_CombatUIPrefab).GetComponent<NewCombatUI>();
    [SerializeField] private GameObject m_CombatUIPrefab;
    
    [SerializeField] private PokemonDefinition[] m_PokemonsToSpawn;
    private static GameManager m_Instance;
    public static GameManager Instance => GetInstance();

    [SerializeField] private float GlobalXPRate = 1;
    
    [SerializeField] private GameObject PokemonPrefab;
    public static GameManager GetInstance()
    {
        if (m_Instance != null)
        {
            return m_Instance;
        }

        m_Instance = FindAnyObjectByType<GameManager>();
        if (m_Instance != null)
        {
            return m_Instance;
        }

        GameObject gameManagerObject = new GameObject("GameManager");
        m_Instance = gameManagerObject.AddComponent<GameManager>();
        return m_Instance;
    }

    private void Awake()
    {
        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private static IEnumerator LoadCombatSceneAndInitialize(PokemonDefinition p_Pokemon1, PokemonDefinition p_Pokemon2)
    { 
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("EscenaDeCombate");
        while (!asyncOperation.isDone) { yield return null; }
        Debug.Log("Cargada escena de combate");
        CombatManager.StartCombat(p_Pokemon1, p_Pokemon2);
        
        //PokemonComponent t_Pokemon1 = SpawnPokemon(p_Pokemon1, new Vector3(0,0,5));
        //PokemonComponent t_Pokemon2 = SpawnPokemon(p_Pokemon2, new Vector3(0,0,0));
        //t_Pokemon1.transform.LookAt(t_Pokemon2.transform, Vector3.up);
        //t_Pokemon2.transform.LookAt(t_Pokemon1.transform, Vector3.up);;
        
    }

    public static void StartCombatWithRandomPokemon(PokemonDefinition p_Pokemon1)
    {
        PokemonDefinition p_Pokemon2 = GetRandomPokemon();
        Debug.Log($"Combate entre {p_Pokemon1.PokemonName} y {p_Pokemon2.PokemonName}");
        Instance.StartCoroutine(LoadCombatSceneAndInitialize(p_Pokemon1, p_Pokemon2));
    }

    public static PokemonDefinition GetRandomPokemon()
    {
        return Instance.m_PokemonsToSpawn[Random.Range(0, Instance.m_PokemonsToSpawn.Length)];
    }
    public static PokemonComponent SpawnPokemon(PokemonDefinition p_Pokemon, Vector3 p_Position)
    {
        PokemonComponent pokemonComponent = Instantiate(GetInstance().PokemonPrefab, p_Position, Quaternion.identity).GetComponent<PokemonComponent>();
        pokemonComponent.Initialize(p_Pokemon);
        return pokemonComponent;
    }
}