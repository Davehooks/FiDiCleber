using UnityEngine;

public class TriggerCapsula : MonoBehaviour
{
    [SerializeField] private GameObject Capsule;
    [SerializeField] private GameObject Cientist;
    private Animator CapsuleAnimator;
    private bool _Opened;


    private void Start()
    {
        CapsuleAnimator = Capsule.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!_Opened)
            {
                CapsuleAnimator.SetTrigger("Entrou");
            }
            else
            {
                Cientist.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Cientist.SetActive(false);
        }
    }
}
