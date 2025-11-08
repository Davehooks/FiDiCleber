

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    //variaveis
    [Header("Movimenta��o")]
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float _crouchSlow = 0.7f;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _impulseJump = 2f;
    [SerializeField] private bool _isGrounded = true;

    [Header("Som")]
    [SerializeField] private PlayerSFX soundScript;

    [SerializeField] public bool _isFacingRight = false;
    [SerializeField] private bool _isBeingHit = false;
    [SerializeField] private bool _isCrouching = false;

    public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }
    public int CurrentHealth { get => currentHealth; set
        {
            currentHealth = value;  
           if(currentHealth < 0)
             {
                Destroy(gameObject);
            }
        } }

    //Animação
    [Header("Animação")]

    [SerializeField] private ParticleSystem _particleJump;
    [SerializeField] private RuntimeAnimatorController[] _animators;
    [SerializeField] private Animator _currentAnimator;
    private SpriteRenderer _spriteRenderer;

    [Header("Camera")]
    [SerializeField]private GameObject _cameraFollowGO;
    private CameraFollowObject _cameraFollowObject;
    //private float _fallSpeedYDampingChangeThreshold;


    //componentes
    private Rigidbody2D _rb;

    [Header("Estado")]
    //Estados
    [SerializeField] ModeState currentModeState;


    //Metodos da Unity
    void Start()
    {
        CurrentHealth = maxHealth;
        if (_currentAnimator == null)
            _currentAnimator = GameObject.Find("Player").GetComponent<Animator>();
        if (_spriteRenderer == null)
            _spriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        if (_rb == null)
            _rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        //currentModeState = ModeState.Normal; // TODO tirar esse comentário pra sempre começar Normal

        _cameraFollowObject = _cameraFollowGO.GetComponent<CameraFollowObject>();
        soundScript = gameObject.GetComponent<PlayerSFX>();

        //_fallSpeedYDampingChangeThreshold = CameraManager.instance._fallSpeedYDampingChangeThresold;
    }


    void FixedUpdate()
    {
       
        if (!_isBeingHit)
        {
            Move();
            AnimationFunc();
        }
        if (moveInput.x > 0 || moveInput.x < 0)
        {
            TurnCheck();
        }
        //Se a gente tá caindo já depois de uma velocidade
        //if (_rb.linearVelocityY < _fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        //{
        //    CameraManager.instance.LerpYDaping(true);
        //}

        //if(_rb.linearVelocityY >= 0 && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        //{
        //    CameraManager.instance.LerpedFromPlayerFalling = false;

        //    CameraManager.instance.LerpYDaping(false);
        //}



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
        if (input.started && IsGrounded && !_isBeingHit)
        {
            _particleJump.Play();

            if (currentModeState != ModeState.Agility)
            {
                IsGrounded = false;
                _rb.AddForceY(_jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                IsGrounded = false;
                Debug.Log("Pulo com double");
                _rb.AddForceY(_jumpForce + _impulseJump, ForceMode2D.Impulse);
            }
        }
    }

    //Methods
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
    public void GetDamage()
    {
        if(CurrentHealth >= 0)
        {
            CurrentHealth--;
            soundScript.PlayDamage(false);
            //TODO arremesa pro lado
            
        }
        else
        {
            soundScript.PlayDamage(true);
            SceneManager.LoadScene("SampleScene"); 
        } //TODO quando fizer o UIManager chamar a tela de morte
    }
    void Action1()
    {
        if (!_isBeingHit && currentModeState != ModeState.Normal)
        {
            _currentAnimator.SetTrigger("Action1");
        }
        if (currentModeState == ModeState.Agility) // Aqui ele dá dash
        {
            soundScript.PlayDash();

        }

        if (currentModeState == ModeState.Defense) // Aqui ele reflete
        {
            soundScript.PlayReflect();

        }

        if (currentModeState == ModeState.Attack) // Aqui ele atacaMelee
        {
            soundScript.PlayMeleeAttack();

        }

    }
    void Action2()
    {
        if (!_isBeingHit && (currentModeState != ModeState.Agility || currentModeState != ModeState.Normal))
        {
            _currentAnimator.SetTrigger("Action2");

            if (currentModeState == ModeState.Defense) // bloqueia e cria um escudo em volta
            {
                soundScript.PlayBlock();

            }

            if (currentModeState == ModeState.Attack)// atira um proj[etil da mão
            {
                soundScript.PlayRangedAttack();

            }

        }
    }
    private void ToCrouch()
    {
        if (!_isBeingHit)
        {
            _isCrouching = !_isCrouching;
            _currentAnimator.SetBool("IsCrouching", _isCrouching);
        }
    }

    private void TurnCheck()
    {
        if (moveInput.x > 0 && !_isFacingRight)
        {
            Turn();

            _cameraFollowObject.CallTurn();
        }
        else if (moveInput.x < 0 && _isFacingRight)
        {
            Turn();

            _cameraFollowObject.CallTurn();
        }
    }
    private void Turn()
    {
        if (_isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
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

    //Anima��o
    private void AnimationFunc()
    {
        //Puxa os modos do personagem e coloca na cor certa
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
        _currentAnimator.SetBool("IsGrounded", IsGrounded); // mostra se t� no ch�o ou n�o

        _currentAnimator.SetBool("IsCrouching", _isCrouching); // t� agachado ou n�o

        if (moveInput.x != 0f && IsGrounded) // t� se movendo
        {
            _currentAnimator.SetBool("IsWalking", true);
        }
        else if (moveInput.x == 0 && IsGrounded) // n�o t� se movendo
        {
            _currentAnimator.SetBool("IsWalking", false);
        }

    }
}
