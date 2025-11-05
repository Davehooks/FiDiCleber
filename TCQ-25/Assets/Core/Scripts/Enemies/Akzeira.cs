using UnityEngine;

public class Akzeira : MonoBehaviour
{
    [SerializeField] private int health;
    //[SerializeField] private float cadenciaTiro;
    [SerializeField] private GameObject _tiroPrefab;
    [SerializeField] private Vector3 weaponOffSet;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _Collider;
    [SerializeField] private float maxDistance = 5.0f;
    [SerializeField] private Vector2 origin;
    [SerializeField] private float _speed;

    void Start()
    {
        origin = transform.position;
    }

    void FixedUpdate()
    {
        //Todo raycast
        if (transform.position.x < (origin.x + maxDistance))
            _rb.linearVelocityX = _speed * Time.fixedDeltaTime;
        else if (transform.position.x >= (origin.x - maxDistance+1))
            _rb.linearVelocityX = -_speed * Time.fixedDeltaTime;
    }

    private void Shoot()
    {
        Instantiate(_tiroPrefab, this.transform.position + weaponOffSet, Quaternion.identity);
    }
}
