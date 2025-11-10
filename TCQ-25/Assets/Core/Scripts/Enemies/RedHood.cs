using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class RedHood : Enemy
{
    [Header("RedHood Variabels")]

    [SerializeField] private Transform groundCheck; //detecta o chão
    [SerializeField] private Transform visionCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float checkRadius = 0.1f; //bem baixo para ser preciso
    
    private bool isFacingRight = false;
    [SerializeField] private float shootingTime = 0.2f;



    //Override methods
    //RedHood vai se mover observando plataformas
    public override void Move()
    {
        float direction = isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if (Physics2D.Linecast(visionCheck.position, transform.position, playerLayer))
        {
            Debug.Log("É tíru!");
            Atack();
        }
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

    //Metodos
    private void Atack()
    {
        StartCoroutine(Shooting(shootingTime));
    }



    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    //VISUALIZACAO NO EDITOR
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, groundCheck.position);
        }
        if (visionCheck != null)
        {
            Gizmos.color = Color.aliceBlue;
            Gizmos.DrawLine(transform.position, visionCheck.position);
        }
    }
    //COROUTINES
    public IEnumerator Shooting(float timing)
    {
        //TODOOOOO
        yield return new WaitForSeconds(timing);
    }
}