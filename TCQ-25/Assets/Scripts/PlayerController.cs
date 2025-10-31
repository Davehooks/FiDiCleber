using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Rigidbody2D _rb;
    [SerializeField]
    private enum Mode
    {
        Agility,
        Defense,
        Attack
    }[SerializeField]Mode currentMode;
    void Start()
    {
        if (_rb == null)
            _rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        
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
        if (input.performed && currentMode != Mode.Agility)
        {
            
            _rb.AddForceY(_jumpForce, ForceMode2D.Impulse);
        }
        else
        {
        Debug.Log("Pulo com double");
            _rb.AddForceY(_jumpForce, ForceMode2D.Impulse);
            _rb.AddForceY(_jumpForce, ForceMode2D.Impulse);
        }
    }
    private void Move()
    {
        _rb.linearVelocityX = moveInput.x * _speed * Time.deltaTime;
        
    }
    
}
