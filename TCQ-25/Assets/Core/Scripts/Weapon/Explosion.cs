using UnityEngine;

public class Explosion : MonoBehaviour
{
    float tamExplosao = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    void FixedUpdate()
    {
         while(tamExplosao < 20.0f)
        {
            transform.localScale *= 2;
            tamExplosao *= 2 * Time.fixedDeltaTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
