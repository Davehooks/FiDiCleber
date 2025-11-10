using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerCapsula : MonoBehaviour
{
    [SerializeField] private GameObject Capsule;
    [SerializeField] private GameObject Cientist;
    private Animator CapsuleAnimator;
    private bool _Opened = false;
    public bool _OpenedEntire = false;
    [SerializeField] Dialog dialog;


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
                _Opened = true;
            }
            else if (_OpenedEntire)
            {
                Cientist.SetActive(true);
            }
        }
    }

    public void Interact(InputAction.CallbackContext input)
    {
        if (Cientist.activeInHierarchy && input.performed)
        {
            dialog.NextPhrase();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialog._speakIndex = 0;
            Cientist.SetActive(false);
            Dialog CientistDialog = Cientist.GetComponent<Dialog>();
            CientistDialog._speakText.text = "";
        }
    }
}
