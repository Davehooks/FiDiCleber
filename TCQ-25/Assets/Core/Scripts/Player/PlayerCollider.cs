using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public LayerMask groundLayer;
    [SerializeField] private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerController.IsGrounded = true;
        }
    }
   /* void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerController.IsGrounded = true;
        }
    }
    */
}
