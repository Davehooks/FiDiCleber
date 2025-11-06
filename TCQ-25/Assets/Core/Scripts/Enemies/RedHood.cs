using System.Buffers.Text;
using UnityEditor.Tilemaps;
using UnityEngine;

public class RedHood : Enemy
{
    [Header("RedHood Variabels")]
    
    [SerializeField] private Transform groundCheck; //detecta o chão
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius = 0.1f; //bem baixo para ser preciso

    private bool isFacingRight = false;

    //Override methods
    //RedHood vai se mover observando plataformas
    public override void Move()
    {
        float direction = isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if (!Physics2D.Linecast(groundCheck.position, transform.position, groundLayer))
        {
            Flip();
        }
    }
    
    protected override void OnHitAnimation(int amountDamage, GameObject source)
    {
        base.OnHitAnimation(amountDamage, source);
        Debug.Log($"{name} foi atingido! Vida atual: {currentHealth}");
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log($"{name} morreu!");
    }
    
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    private void OnDrawGizmosSelected()
{
    if (groundCheck != null)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}

}