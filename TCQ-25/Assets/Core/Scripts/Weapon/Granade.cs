using UnityEngine;

public class Granade : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float speed = 7f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 3f;

    [SerializeField] private RedHood _redHood;
    [SerializeField] private GameObject _fire;
    private void Start()
    {
        if(_redHood == null)
        _redHood = GameObject.FindFirstObjectByType<RedHood>();
       float direction = _redHood.IsFacingRight() ? 1f : -1f;
        velocity = new Vector2(direction * speed, 0f);
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {

        transform.position += (Vector3)velocity * Time.deltaTime;
        if (lifetime <= 0)
        {
            Instantiate(_fire, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"{lifetime}");
            lifetime -= Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
