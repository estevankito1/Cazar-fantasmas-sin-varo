using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turn
{
    PokemonComponent m_Attacker;
    PokemonComponent m_Receiver;
    PokemonMove m_MoveUsed;

    State m_Attack;
    State m_GetDamaged;

    public Turn(PokemonComponent p_Attacker, PokemonComponent p_Receiver, PokemonMove p_MoveUsed)
    {
        m_Attacker = p_Attacker;
        m_Receiver = p_Receiver;
        m_MoveUsed = p_MoveUsed;
        
        m_Attack = new AttackState(this);
        m_GetDamaged = new DamageState(this);
    }

    public void StartTurn()
    {
        CombatManager.Instance.ChangeState(m_Attack);
    }

    public class AttackState : State
    {
        Turn m_Turn;
        Timer m_Timer;

        public AttackState(Turn p_Turn)
        {
            m_Turn = p_Turn;
        }
        public override void Enter()
        {
            CombatManager.Instance.m_UI.SetTextBox($"{m_Turn.m_Attacker.Information.Name} usó {m_Turn.m_MoveUsed.MoveName} contra {m_Turn.m_Receiver.Information.Name}!");
            m_Timer = new Timer(3f);
            //Animate Attacker
        }
        public override void FixedUpdate()
        {
            //When the animation is done
            m_Timer.Tick(Time.fixedDeltaTime);
            if (m_Timer.IsFinished) CombatManager.Instance.ChangeState(m_Turn.m_GetDamaged);
        }
        public override void Update()
        {
            
        }
        public override void Exit()
        {
            
        }
    }

    public class DamageState : State
    {
        Turn m_Turn;
        Timer m_Timer;
        
        Timer m_TransitionTimer; // Timer para la transición
        bool m_IsFainted = false;

        public DamageState(Turn p_Turn)
        {
            m_Turn = p_Turn;
        }
        public override void Enter()
        {
            int damage = CombatManager.CalculateDamage(m_Turn.m_MoveUsed, m_Turn.m_Attacker.Information,  m_Turn.m_Receiver.Information);
            m_Turn.m_Receiver.Information.GetDamage(damage);
            
            CombatManager.Instance.m_UI.SetTextBox($"{m_Turn.m_Receiver.Information.Name} recibió {damage} de daño!");
            
            m_Timer = new Timer(3f);
            //Animate Attacker
        }

        public override void FixedUpdate()
        {
            m_Timer.Tick(Time.fixedDeltaTime);
            if (m_Timer.IsFinished) 
            {
                if (m_Turn.m_Receiver.Information.currentHealth <= 0)
                {
                    if (!m_IsFainted)
                    {
                        m_IsFainted = true;
                        CombatManager.Instance.m_UI.SetTextBox($"{m_Turn.m_Receiver.Information.Name} ha perdido el combate!");
                        m_TransitionTimer = new Timer(3f); // Esperar 3 segundos antes de cambiar de escena
                    }
                    else if (m_TransitionTimer != null)
                    {
                        m_TransitionTimer.Tick(Time.fixedDeltaTime);
                        if (m_TransitionTimer.IsFinished)
                        {
                            // Cargar la escena anterior
                            SceneManager.LoadScene("Scenes/World");
                        }
                    }
                }
                else
                {
                    CombatManager.PlayNextTurn();
                }
            }
        }
        public override void Update()
        {
            
        }
        public override void Exit()
        {
            
        }
    }
}