using UnityEngine;
using UnityEngine.SceneManagement;

public class Grass : MonoBehaviour
{
    [SerializeField] private string EscenaDeCombate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player p_MainCharacter))
        {
            Debug.Log(p_MainCharacter.Pokemon.PokemonName);
            GameManager.StartCombatWithRandomPokemon(p_MainCharacter.Pokemon);
        }
    }
}
