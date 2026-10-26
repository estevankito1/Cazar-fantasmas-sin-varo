using System.Collections.Generic;
using UnityEngine;

public class CombatManager : StateMachine
{
    #region Singleton

        public static CombatManager Instance => GetInstance();
        private static CombatManager m_Instance;
    
        private static CombatManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = FindAnyObjectByType<CombatManager>();
                if (m_Instance == null)
                {
                GameObject obj = new GameObject("CombatManager");
                m_Instance = obj.AddComponent<CombatManager>();
                }
            }

            return m_Instance;
        }
    #endregion
    
    public Queue<Turn> turnQueue = new Queue<Turn>();

    public PokemonComponent playerPokemon;
    public PokemonComponent enemyPokemon;
    
    public PokemonMove playerMove;

    public NewCombatUI m_UI;

    public static void SetPlayerMove(PokemonMove p_Move)
    {
        Instance.playerMove = p_Move;
    }
    public static void StartCombat(PokemonDefinition p_Pokemon1, PokemonDefinition p_Pokemon2)
    {

        Debug.Log("Combate entre " + p_Pokemon1.PokemonName + " y " + p_Pokemon2.PokemonName);
        
        // Posición del Pokemon del jugador (izquierda)
        Vector3 playerPosition = new Vector3(-2, 0, 0);
        // Posición del Pokemon enemigo (derecha)
        Vector3 enemyPosition = new Vector3(2, 0, 0);
    
        // Spawn del Pokemon del jugador mirando hacia la derecha
        Instance.playerPokemon = GameManager.SpawnPokemon(p_Pokemon1, playerPosition);
        Instance.playerPokemon.transform.rotation = Quaternion.Euler(0, 90, 0);
    
        Debug.Log(Instance.playerPokemon.Information.Name);
    
        // Spawn del Pokemon enemigo mirando hacia la izquierda
        Instance.enemyPokemon = GameManager.SpawnPokemon(p_Pokemon2, enemyPosition);
        Instance.enemyPokemon.transform.rotation = Quaternion.Euler(0, -90, 0);

        
        Instance.m_UI = GameManager.NewCombatUI;
        Instance.m_UI.Initialize(Instance.playerPokemon.Information);
        StartNewRound();
        
    }

    public static void StartNewRound()
    {
        Instance.turnQueue.Clear();
        Instance.playerMove = null;
        
        Instance.ChangeState(new WaitForActionState());
    }

    public void BuildTurnQueue()
    {
        PokemonComponent fastestPokemon;
        PokemonComponent slowestPokemon;
        PokemonMove fastestMove, slowestMove;

        if (Instance.playerPokemon.Information.Speed > Instance.enemyPokemon.Information.Speed)
        {
            fastestPokemon = Instance.playerPokemon;
            fastestMove = Instance.playerMove;
            slowestPokemon = Instance.enemyPokemon;
            slowestMove = Instance.enemyPokemon.UseRandomMove();
        }
        else
        {
            fastestPokemon = Instance.enemyPokemon;
            fastestMove = Instance.enemyPokemon.UseRandomMove();
            slowestPokemon = Instance.playerPokemon;
            slowestMove = Instance.playerMove;
        }

        Instance.turnQueue.Enqueue(new Turn(fastestPokemon, slowestPokemon, fastestMove));
        Instance.turnQueue.Enqueue(new Turn(slowestPokemon, fastestPokemon, slowestMove));
    }

    public static void PlayNextTurn()
    {
        if (Instance.turnQueue.Count == 0)
        {
            StartNewRound();
        }
        else
        {
            Turn t_NextTurn = Instance.turnQueue.Dequeue();
            t_NextTurn.StartTurn();
        }
    }

    public static int CalculateDamage(PokemonMove move, PokemonInformation attacker, PokemonInformation receiver)
    {
        if (move.IsSpecial)
        {
            return 5 + move.Power * (attacker.SpAttack / receiver.SpDefense);
        }
        else
        {
            return 5 + move.Power * (attacker.Attack / receiver.Defense);
        }
    }
}