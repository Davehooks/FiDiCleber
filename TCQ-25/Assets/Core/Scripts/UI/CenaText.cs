using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CenaText : MonoBehaviour
{
    private bool entrou = false;
    [SerializeField] private TMP_Text text;  

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (!entrou)
        {
        text.gameObject.SetActive(true);
        entrou = true;
        }
    }

    IEnumerator CallText()
    {
        new WaitForSeconds(5);
        text.gameObject.SetActive(false);
        for (int index = 0; index <= 5; index++)
        {
            text.color = new Color (text.color.r,text.color.g,text.color.b,text.color.a -0.2f);
            new WaitForSeconds(0.2f);
        }

        yield return null;
    }
}
