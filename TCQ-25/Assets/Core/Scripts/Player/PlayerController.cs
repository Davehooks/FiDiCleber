using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //variaveis
    [Header("Movimentação")]

    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float _crouchSlow = 0.7f;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _impulseJump = 2f;
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private bool _isFacingRight = false;
    [SerializeField] private bool _isBeingHit = false;
    [SerializeField] private bool _isCrouching = false;



    [Header("Animacao")]

    [SerializeField] private RuntimeAnimatorController[] _animators;
    [SerializeField] private Animator _currentAnimator;
    private SpriteRenderer _spriteRenderer;



    //componentes
    private Rigidbody2D _rb;

    //Estados
    [SerializeField] ModeState currentModeState;

    void Start()
    {
        if (_currentAnimator == null)
            _currentAnimator = GameObject.Find("Player").GetComponent<Animator>();
        if (_spriteRenderer == null)
            _spriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        if (_rb == null)
            _rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        currentModeState = ModeState.Normal;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isBeingHit)
        {
        Move();
        AnimationFunc();
        }
    }

    //Events -- Input Player
    public void Walk(InputAction.CallbackContext input)
    {
        moveInput = input.ReadValue<Vector2>();
    }

    public void InputAction1(InputAction.CallbackContext input)
    {
        Action1();
    }

    public void InputAction2(InputAction.CallbackContext input)
    {
        Action2();
    }

    public void Crouch(InputAction.CallbackContext input)
    {
        ToCrouch();
    }

    public void Jump(InputAction.CallbackContext input)
    {
        if (input.started)
        {

            if (currentModeState != ModeState.Agility)
            {
                
                _rb.AddForceY(_jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Pulo com double");
                _rb.AddForceY(_jumpForce + _impulseJump, ForceMode2D.Impulse);
            }
        }
    }

    private void Move()
    {
        if (_isCrouching)
        {
            _rb.linearVelocityX = moveInput.x * _speed * _crouchSlow * Time.deltaTime;
        }
        else
        {
            _rb.linearVelocityX = moveInput.x * _speed * Time.deltaTime;
        }

    }

    void Action1()
    {
        if (!_isBeingHit)
        {
            _currentAnimator.SetTrigger("Action1");
        }

        if (currentModeState == ModeState.Normal)
        {


        }

        if (currentModeState == ModeState.Agility)
        {


        }

        if (currentModeState == ModeState.Defense)
        {


        }

        if (currentModeState == ModeState.Attack)
        {


        }

    }

    void Action2()
    {
        if (!_isBeingHit && currentModeState != ModeState.Agility)
        {
            _currentAnimator.SetTrigger("Action2");

            if (currentModeState == ModeState.Normal)
            {


            }

            if (currentModeState == ModeState.Defense)
            {


            }

            if (currentModeState == ModeState.Attack)
            {


            }

        }
    }

    private void ToCrouch()
    {
        if (_isGrounded && !_isBeingHit)
        {
            _isCrouching = !_isCrouching;
            _currentAnimator.SetBool("IsCrouching", _isCrouching);
        }
    }

    //ENUMS
    private enum ModeState
    {
        Normal,
        Agility,
        Defense,
        Attack
    }

    

    /*
        private enum movementState
        {
            facingRight,
            jumping,
            crouching
        }
    */

    //Animação

    private void AnimationFunc() 
    {
        //Puxa os modos do personagem e coloca na cor certa
        switch (currentModeState) {
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
        


        _currentAnimator.SetBool("IsGrounded", _isGrounded); // mostra se tá no chão ou não
        
        _currentAnimator.SetBool("IsCrouching", _isCrouching); // tá agachado ou não
        
        if (moveInput.x != 0f && _isGrounded) // tá se movendo
        {
            _currentAnimator.SetBool("IsWalking", true);
            if (moveInput.x>0f)
            {
                _spriteRenderer.flipX = false;
            }
            else
            {
                _spriteRenderer.flipX = true;
                _isFacingRight = false;
            }
        }
        else if (moveInput.x == 0 && _isGrounded) // não tá se movendo
        {
            _currentAnimator.SetBool("IsWalking", false);
        }

    }
}
