using UnityEngine;
using UnityEngine.UI;

public class ActivateDeactivateGO : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObject;
    [SerializeField] private Image[] _imagens;

    public void DeactivateThisGO()
    {
        this.gameObject.SetActive(false);
    }
    public void DeactivateGO()
    {
        for (int i = 0; i < _gameObject.Length; i++)
        {
            _gameObject[i].SetActive(false);
        }
    }
    public void ActivateGO()
    {
        for (int i = 0; i < _gameObject.Length; i++)
        {
            _gameObject[i].SetActive(true);
        }
    }

    public void ActivateImage()
    {
        for (int i = 0; i < _imagens.Length; i++)
        {
            _imagens[i].enabled = true;
        }
    }

}
