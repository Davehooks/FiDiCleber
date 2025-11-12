using Unity.VisualScripting;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [Header("Animação")]
    [SerializeField]private PlayerController player;
    [SerializeField] private RuntimeAnimatorController[] _animators;
    [SerializeField] private Animator _currentAnimator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem[] _particle;
    private void Start()
    {
        player = GetComponent<PlayerController>();
        _currentAnimator = GetComponent<Animator>();
    }
    public void AnimationFunc(int currentModeState, bool IsGrounded, bool _isCrouching, Vector2 moveInput)
    {
        //Puxa os modos do personagem e coloca na cor certa
        switch (currentModeState)
        {
            case 0:
                _currentAnimator.runtimeAnimatorController = _animators[0];
                break;
            case 1:
                _currentAnimator.runtimeAnimatorController = _animators[1]; // agility
                break;
            case 2:
                _currentAnimator.runtimeAnimatorController = _animators[2]; //defense
                break;

            case 3:
                _currentAnimator.runtimeAnimatorController = _animators[3]; // attack
                break;
        }
        _currentAnimator.SetBool("IsGrounded", IsGrounded); // mostra se t� no ch�o ou n�o

        _currentAnimator.SetBool("IsCrouching", _isCrouching); // t� agachado ou n�o

        if (moveInput.x != 0f && player.IsGrounded) // t� se movendo
        {
            _currentAnimator.SetBool("IsWalking", true);
        }
        else if (moveInput.x == 0 && IsGrounded) // n�o t� se movendo
        {
            _currentAnimator.SetBool("IsWalking", false);
        }

    }
    public void PlayAction1()
    {
        if(_currentAnimator.runtimeAnimatorController !=_animators[0])
        {
            _currentAnimator.SetTrigger("Action1");
        }
    }

    public void PlayAction2()
    {
        if (_currentAnimator.runtimeAnimatorController != _animators[0] || _currentAnimator.runtimeAnimatorController != _animators[1])
        {
            _currentAnimator.SetTrigger("Action");

        }
    }
    public void IsntHit()
    {
        player._isBeingHit = false;
    }

    public void PlayJump()
    {
        _particle[0].Play();
    }
    public void PlayTurn(bool Right)
    {
        if (!Right)
        {
            if (player.IsGrounded)
            {
                _particle[1].Play();
                _particle[1].transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
            }
        }
        if (Right)
        {
            if (player.IsGrounded)
            {
                _particle[1].Play();
                _particle[1].transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
        }

    }

    public void PlayWakeUp()
    {
        _currentAnimator.SetTrigger("WakeUP");
    }

}
