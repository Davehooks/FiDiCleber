using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //variaveis
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _impulseJump = 2f;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private bool _isFacingRight = false;

    //componentes
    private Rigidbody2D _rb;

    //Estados
    [SerializeField] ModeState currentModeState;

    void Start()
    {
        if (_rb == null)
            _rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        currentModeState = ModeState.Normal;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    //Events -- Input Player
    public void Walk(InputAction.CallbackContext input)
    {
        moveInput = input.ReadValue<Vector2>();
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
        _rb.linearVelocityX = moveInput.x * _speed * Time.deltaTime;

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
}
