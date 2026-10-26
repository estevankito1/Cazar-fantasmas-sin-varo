using System;
using Unity.Mathematics;
using UnityEngine;

public class Player : StateMachine
{
    public Rigidbody rigidBody;
    public Animator animator;
    public LayerMask groundLayer;
    public PokemonDefinition Pokemon;

    private InputSystem_Actions m_InputActions;

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayer);
    }

    public Vector3 MoveDirection()
    {
        Vector3 MoveDirVector = new Vector3(m_InputActions.Player.Move.ReadValue<Vector2>().x, 0f,
            m_InputActions.Player.Move.ReadValue<Vector2>().y);
        return MoveDirVector;
    }

    void Start()
    {
        m_InputActions = new InputSystem_Actions();
        m_InputActions.Enable();

        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        m_CurrentState = new PlayerIdle(this);
    }


    public void Rotate(float p_RotationSpeed)
    {
        float rotation = MoveDirection().x;
        animator.InterpolateFloat("Turn", rotation * 90, p_RotationSpeed);
        if (rotation == 0f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y + rotation * 90f, 0f);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * p_RotationSpeed);
    }

    public void Move(float p_Speed)
    {
        float move = MoveDirection().z;
        if (move <= 0f)
        {
            return;
        }

        rigidBody.linearVelocity = transform.forward * move * p_Speed;
    }

    public void ClampToFloor()
    {
        if (!isGrounded())
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    private void OnDisable()
    {
        m_InputActions.Disable();
    }

    private void OnDestroy()
    {
        m_InputActions.Disable();    
    }
}

public class PlayerIdle : State
{
    Player m_Player;

    public PlayerIdle(Player player)
    {
        m_Player = player;
    }

    public override void Enter()
    {
        Debug.Log("Player is now idle");
        m_Player.animator?.CrossFadeInFixedTime("Idle 2", 0.2f);
        m_Player.rigidBody.linearVelocity = Vector3.zero;
        m_Player.rigidBody.useGravity = false;
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        m_Player.Rotate(5f);

        if (m_Player.MoveDirection().magnitude > 0f)
        {
            m_Player.ChangeState(new PlayerMove(m_Player));
        }

        if (!m_Player.isGrounded())
        {
            m_Player.ChangeState(new PlayerFalling(m_Player));
        }
    }

    public override void Exit()
    {
        Debug.Log("Player is leaving idle state");
    }

    public class PlayerMove : State
    {
        Player m_Player;

        public PlayerMove(Player player)
        {
            m_Player = player;
        }

        public override void Enter()
        {
            Debug.Log("Player is now Moving");
            m_Player.animator?.CrossFadeInFixedTime("Move", 0.2f);
            m_Player.rigidBody.linearVelocity = Vector3.zero;
            m_Player.rigidBody.useGravity = false;
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            m_Player.Rotate(5f);
            m_Player.Move(5f);

            if (m_Player.MoveDirection().magnitude < 0.1f)
            {
                m_Player.ChangeState(new PlayerIdle(m_Player));
            }
            if (!m_Player.isGrounded()) {m_Player.ChangeState(new PlayerFalling(m_Player));}
        }

        public override void Exit()
        {
            Debug.Log("Player is leaving move state");
        }
    }
}

public class PlayerFalling : State
{
    Player m_Player;
    public PlayerFalling(Player player)
    {
        m_Player = player;
    }

    public override void Enter()
    {
        Debug.Log("Player is now falling");
        m_Player.animator?.CrossFadeInFixedTime("Falling,0.2", 0.2f);
        m_Player.rigidBody.useGravity = true;
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {
        if (m_Player.isGrounded())
        {
            m_Player.ChangeState(new PlayerIdle(m_Player));
        }
    }

    public override void Exit()
    {
        m_Player.ClampToFloor();
    }
}

public static class MyExtentions
{
    public static void InterpolateFloat(this Animator animator, string parameter, float value, float speed)
    {
        float current = animator.GetFloat(parameter);
        float direction = Mathf.Sign(value - current);
        current = Mathf.MoveTowards(current, value, speed * Time.deltaTime);
        animator.SetFloat(parameter, current);
    }

    public static void Interpolate(this float me, float target, float speed)
    {
        me = Mathf.MoveTowards(me, target, speed * Time.deltaTime);
    }
}