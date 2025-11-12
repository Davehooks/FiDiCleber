using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPos;
    [SerializeField] private GameObject cam;
    [SerializeField] private float _parallaxEffect;
    [SerializeField] private bool _X, _Y;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
    private void ParallaxY()
    {
        float distance = cam.transform.position.y * _parallaxEffect;

        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);
    }
}
