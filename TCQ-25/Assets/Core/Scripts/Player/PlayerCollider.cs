using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerController.IsGrounded = true;
        }
    }
}
