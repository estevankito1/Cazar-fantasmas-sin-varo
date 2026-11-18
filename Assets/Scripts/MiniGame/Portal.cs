using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneName = "MiniGame";

    [Header("Dice Settings")]
    [Range(1, 6)]
    public int requiredRoll = 4;

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (playerInside)
            return;

        playerInside = true;

        int diceRoll = Random.Range(1, 7);

        Debug.Log("Resultado del dado: " + diceRoll);

        if (diceRoll >= requiredRoll)
        {
            Debug.Log("¡Entraste al minijuego!");

            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("No entraste al minijuego. Puedes volver a intentarlo.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Puedes volver a intentar entrar.");
        }
    }
}
