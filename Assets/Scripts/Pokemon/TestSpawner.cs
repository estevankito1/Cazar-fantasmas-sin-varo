using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] public PokemonDefinition pokemonDefinition1;
    [SerializeField] public PokemonDefinition pokemonDefinition2;
    void Start()
    {
        GameManager.SpawnPokemon(pokemonDefinition1, transform.position);
        GameManager.SpawnPokemon(pokemonDefinition2, transform.position + Vector3.right);
    }
}
