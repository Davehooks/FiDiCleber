using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPos,length;
    [SerializeField] private GameObject cam;
    [SerializeField] private float _parallaxEffect;
    [SerializeField] private bool _X, _Y;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_X) { ParallaxX(); }
        if (_Y) { ParallaxY(); }
    }

    private void ParallaxX()
    {
        float distance = cam.transform.position.x * _parallaxEffect;
        float movement = cam.transform.position.x * (1-_parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
    private void ParallaxY()
    {
        float distance = cam.transform.position.y * _parallaxEffect;

        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);
    }
}
