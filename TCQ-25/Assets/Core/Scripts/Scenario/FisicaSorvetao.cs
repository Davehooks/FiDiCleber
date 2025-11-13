using UnityEngine;

public class FisicaSorvetao : MonoBehaviour
{

    [SerializeField] private Vector2 velocidade,aceleracao;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void FixedUpdate()
    {
        velocidade += aceleracao * Time.fixedDeltaTime;
        transform.position = new Vector2(transform.position.x + velocidade.x, transform.position.y + velocidade.y);
    }
}
