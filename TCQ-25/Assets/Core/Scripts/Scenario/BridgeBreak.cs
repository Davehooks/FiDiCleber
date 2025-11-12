using UnityEngine;

public class BridgeBreak : MonoBehaviour
{
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private Animator[] _bridges;
    void Start()
    {
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < _bridges.Length; i++)
            {
                _bridges[i].SetTrigger("Quebra");
            }
        }
        collider2D.enabled = false;
    }
}
