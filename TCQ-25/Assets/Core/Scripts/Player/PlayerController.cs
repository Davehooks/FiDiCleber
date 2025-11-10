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
    [SerializeField] public float _baseSpeed = 100f;
    [SerializeField] public float _baseJumpForce = 15f;
    [SerializeField] private float _impulseJump = 2f;
    [SerializeField] private float _crouchSlow = 0.7f;

    [Header("Estado")]
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] public bool _isFacingRight = false;
    [SerializeField] private bool _isBeingHit = false;
    [SerializeField] private bool _isCrouching = false;

[Header("Animação")]

    [SerializeField] private ParticleSystem _particleJump;
    [SerializeField] private RuntimeAnimatorController[] _animators;
    [SerializeField] private Animator _currentAnimator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 moveInput;
    
    public Dictionary<ModeState, IPlayerMode> modeInstances;
    private IPlayerMode currentMode;
    private ModeState currentModeState;

    public Rigidbody2D Rigidbody => _rb;
    public Animator Animator => _currentAnimator;
    public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }
    public bool IsBeingHit => _isBeingHit;
    public bool IsCrouching { get => _isCrouching; set => _isCrouching = value; }
    public bool IsFacingRight { get => _isFacingRight; set => _isFacingRight = value; }
    public float Speed { get; set; }
    public float JumpForce { get; set; }
    public float ImpulseJump => _impulseJump;
    public float CrouchSlow => _crouchSlow;
    public int CurrentHealth { get; set; }
    public ParticleSystem ParticleJump => _particleJump;

    public enum ModeState { Normal, Agility, Defense, Attack }

    void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_currentAnimator == null) _currentAnimator = GetComponent<Animator>();
        
        Speed = _baseSpeed;
        JumpForce = _baseJumpForce;
        CurrentHealth = maxHealth;
        InitializeModes();
        SwitchMode(ModeState.Normal);
    }

    void FixedUpdate()
    {
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
        if (input.started && _isGrounded && !_isBeingHit)
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
        AnimationFunc();
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
    private void AnimationFunc()
{
    bool wasWalking = _currentAnimator.GetBool("IsWalking");
    bool wasGrounded = _currentAnimator.GetBool("IsGrounded");
    bool wasCrouching = _currentAnimator.GetBool("IsCrouching");
    
    switch (currentModeState)
    {
        case ModeState.Normal:
            _currentAnimator.runtimeAnimatorController = _animators[0];
            break;
        case ModeState.Agility:
            _currentAnimator.runtimeAnimatorController = _animators[1];
            break;
        case ModeState.Defense:
            _currentAnimator.runtimeAnimatorController = _animators[2];
            break;
        case ModeState.Attack:
            _currentAnimator.runtimeAnimatorController = _animators[3];
            break;
    }
    
    _currentAnimator.SetBool("IsWalking", wasWalking);
    _currentAnimator.SetBool("IsGrounded", wasGrounded);
    _currentAnimator.SetBool("IsCrouching", wasCrouching);
}
}