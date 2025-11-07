using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private Transform PlayerTransform;

    [SerializeField] private float _flipYRotationTime = 0.5f;

    private Coroutine _turnCoroutine;

    private bool _IsFacingRight;


    private void Start()
    {
        PlayerTransform = Player.transform;
        _IsFacingRight = Player._isFacingRight;

    }

    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipYRotationTime).setEaseInOutSine();
    }

    private void FixedUpdate()
    {
        transform.position = PlayerTransform.position;
    }


    private float DetermineEndRotation()
    {
        _IsFacingRight = !_IsFacingRight;

        if (!_IsFacingRight)
        {
            return 180f;
        }

        else
        {
            return 0f;
        }
    }
}
