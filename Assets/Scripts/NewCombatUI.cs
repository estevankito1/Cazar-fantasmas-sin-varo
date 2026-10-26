using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewCombatUI : MonoBehaviour
{

    [SerializeField] private GameObject m_ButtonPrefab;
    [SerializeField] private Transform m_ButtonParent;
    [SerializeField] private TextMeshProUGUI m_TextBox;

    [SerializeField] private Slider barraVidaJugador;
    [SerializeField] private Slider barraVidaEnemigo;
    [SerializeField] private TextMeshProUGUI textoNombreJugador;
    [SerializeField] private TextMeshProUGUI textoVidaJugador;
    [SerializeField] private TextMeshProUGUI textoNombreEnemigo;
    [SerializeField] private TextMeshProUGUI textoVidaEnemigo;

    

    public void Initialize(PokemonInformation pokemonInformation)
    {
        if (pokemonInformation.Moves == null || pokemonInformation.Moves.Length == 0)
        {
            Debug.LogError("Este chafamon no tiene movimientos");
            return;
        }

        foreach (var move in pokemonInformation.Moves)
        {
            NewMoveButton button = Instantiate(m_ButtonPrefab, m_ButtonParent).GetComponent<NewMoveButton>();
            button.Initialize(move);
        }
        
        //ActualizarBarrasDeVida(CombatManager.Instance.playerPokemon.Information, CombatManager.Instance.enemyPokemon.Information);

    }
    private void Update()
    {
        // Actualizar barras de vida cada frame
        if (CombatManager.Instance != null && 
            CombatManager.Instance.playerPokemon != null && 
            CombatManager.Instance.enemyPokemon != null)
        {
            ActualizarUI(
                CombatManager.Instance.playerPokemon.Information,
                CombatManager.Instance.enemyPokemon.Information
            );
        }
    }

    private void ActualizarUI(PokemonInformation jugador, PokemonInformation enemigo)
    {
        // Actualizar barras de vida
        if (barraVidaJugador != null)
        {
            barraVidaJugador.maxValue = jugador.maxHealth;
            barraVidaJugador.value = jugador.currentHealth;
        }

        if (barraVidaEnemigo != null)
        {
            barraVidaEnemigo.maxValue = enemigo.maxHealth;
            barraVidaEnemigo.value = enemigo.currentHealth;
        }

        // Actualizar textos de nombre
        if (textoNombreJugador != null)
        {
            textoNombreJugador.text = jugador.Name;
        }

        if (textoNombreEnemigo != null)
        {
            textoNombreEnemigo.text = enemigo.Name;
        }

        // Actualizar textos de vida
        if (textoVidaJugador != null)
        {
            textoVidaJugador.text = $"{jugador.currentHealth}/{jugador.maxHealth}";
        }

        if (textoVidaEnemigo != null)
        {
            textoVidaEnemigo.text = $"{enemigo.currentHealth}/{enemigo.maxHealth}";
        }
    }

    
    public void SetTextBox(string message)
    {
        if (m_TextBox != null)
        {
            m_TextBox.text = message;
        }
    }
}
