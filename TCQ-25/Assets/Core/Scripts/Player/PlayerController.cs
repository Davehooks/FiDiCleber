using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Componentes")]
    
    [SerializeField] private Rigidbody2D _rb;
    
    [Header("Estatísticas")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth = 3;
    public  float _baseSpeed = 1000f;
    public  float _baseJumpForce = 15f;
    [SerializeField] private float _impulseJump = 2f;
    [SerializeField] private float _crouchSlow = 0.7f;

    [Header("Som")]
    [SerializeField] private CallSFX soundScript;
    
    [Header("Estado")]
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] public bool _isFacingRight = false;
    [SerializeField] public bool _isBeingHit = false;
    [SerializeField] private bool _isCrouching = false;

[Header("Animação")]


    private Vector2 moveInput;
    
    public Dictionary<ModeState, IPlayerMode> modeInstances;
    private IPlayerMode currentMode;
    private ModeState currentModeState;

    public Rigidbody2D Rigidbody => _rb;
    public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }
    public bool IsBeingHit => _isBeingHit;
    public bool IsCrouching { get => _isCrouching; set => _isCrouching = value; }
    public bool IsFacingRight { get => _isFacingRight; set => _isFacingRight = value; }
    public float Speed { get; set; }
    public float JumpForce { get; set; }
    public float ImpulseJump => _impulseJump;
    public float CrouchSlow => _crouchSlow;
    public int CurrentHealth { get; set; }

    public enum ModeState { Normal, Agility, Defense, Attack }

    void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        Debug.Log($"base velocidade inicio:{_baseSpeed}");
        Speed = _baseSpeed;
        Debug.Log($"velocidade inico:{Speed}");
        JumpForce = _baseJumpForce;
        CurrentHealth = maxHealth;
        InitializeModes();
        SwitchMode(ModeState.Normal);
    }

    void FixedUpdate()
    {
        Debug.Log($"base velocidade fixed: {_baseSpeed}");
        Debug.Log($"velocidade fixed: {Speed}");
        if (!_isBeingHit)
        {
            currentMode?.HandleMovement(moveInput);
            currentMode?.UpdateAnimations();
            
            if (moveInput.x > 0 || moveInput.x < 0)
            {
                TurnCheck();
            }
        }
    }

    public void Walk(InputAction.CallbackContext input)
    {
        moveInput = input.ReadValue<Vector2>();
    }

    public void InputAction1(InputAction.CallbackContext input)
    {
        if (input.started) currentMode?.HandleAction1();
    }

    public void InputAction2(InputAction.CallbackContext input)
    {
        if (input.started) currentMode?.HandleAction2();
    }

    public void Crouch(InputAction.CallbackContext input)
    {
        if (input.started) currentMode?.HandleCrouch();
    }

    public void Jump(InputAction.CallbackContext input)
    {
        if (_isGrounded && input.performed &&  !_isBeingHit)
        {
            currentMode?.HandleJump();
        }
    }

    private void InitializeModes()
    {
        modeInstances = new Dictionary<ModeState, IPlayerMode>
        {
            { ModeState.Normal, new NormalMode() },
            { ModeState.Agility, new AgilityMode() },
            { ModeState.Defense, new DefenseMode() },
            { ModeState.Attack, new AttackMode() }
        };
    }

    public void SwitchMode(ModeState newMode)
    {
        currentMode?.ExitMode();
        currentModeState = newMode;
        currentMode = modeInstances[newMode];
        currentMode.EnterMode(this);
    }

    private void TurnCheck()
    {
        if (moveInput.x > 0 && !_isFacingRight)
        {
            Turn();
        }
        else if (moveInput.x < 0 && _isFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (_isFacingRight)
    {
        transform.localScale = new Vector3(-1, 1, 1);
        _isFacingRight = false;
    }
    else
    {
        transform.localScale = new Vector3(1, 1, 1);
        _isFacingRight = true;
    }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}