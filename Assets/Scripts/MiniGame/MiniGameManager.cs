
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MiniGameManager : MonoBehaviour
{
    [Header("Player Settings")]
    public int lives = 3;

    [Header("Victory Settings")]
    public int kills = 0;
    public int killsToWin = 10;

    [Header("Scene Settings")]
    public float returnDelay = 2f;

    public bool gameOver = false;

    public void TakeDamage()
    {
        if (gameOver) return;

        lives--;

        Debug.Log("Vidas restantes: " + lives);

        if (lives <= 0)
        {
            // Perder el minijuego resta 4 puntos de cordura
            VisibilityController.ReducirCorduraGlobal(4);

            StartCoroutine(ReturnToWorld("GAME OVER"));
        }
    }

    public void RegisterKill()
    {
        if (gameOver) return;

        kills++;

        Debug.Log("Enemigos eliminados: " + kills);

        if (kills >= killsToWin)
        {
            StartCoroutine(ReturnToWorld("¡VICTORIA!"));
        }
    }

    IEnumerator ReturnToWorld(string message)
    {
        gameOver = true;

        Debug.Log(message);

        yield return new WaitForSeconds(returnDelay);

        Time.timeScale = 1f;

        SceneManager.LoadScene("World");
    }
}
