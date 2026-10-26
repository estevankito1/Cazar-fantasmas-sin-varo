using UnityEngine;
using UnityEngine.SceneManagement;

public class Grass : MonoBehaviour
{

    int enemyRoll = 0;

    [SerializeField] private string EscenaDeCombate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player p_MainCharacter))
        {
            enemyRoll  = Random.Range (0 , 100);

            if (enemyRoll < 5)
            {

                Debug.Log(p_MainCharacter.Pokemon.PokemonName);
                GameManager.StartCombatWithRandomPokemon(p_MainCharacter.Pokemon);
                Debug.Log(enemyRoll);
            }
        }
    }
}