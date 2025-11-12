using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _quadrinhos; // Hq das historinhas
    [SerializeField] private Image _timeLater; //Tempo depois
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clip; // colocar som de raio e explosão
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _fabricaAnimator;
    [SerializeField] private int _indexCutscene = 0;
    [SerializeField] private int _tempoCutscenes=5;
    [SerializeField] private GameObject[] globalLights;
    [SerializeField] private Animations _playerAnimations;
    [SerializeField] private GameObject[] _cameras;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cameras[0].gameObject.SetActive(false);
        _cameras[1].gameObject.SetActive(true);
        nextScene();
    }

    public void NextScene(InputAction.CallbackContext botao)
    {
        if(botao.performed)
        {
            StopAllCoroutines();
            nextScene();
        }
    }

    private void nextScene()
    {
        StartCoroutine(CallNextScene());
        _indexCutscene++;
        if (_indexCutscene<=3)
        {
        _quadrinhos[_indexCutscene].SetActive(true);
            Debug.Log("Chamou os quadrinhos");
        }
        if(_indexCutscene == 4)
        {
            Debug.Log(" Desabilitando os quadrinhos");
            for (int i = 1; i <= 3; i++)
            {
                _quadrinhos[i].SetActive(false);
            }
        }
        if(_indexCutscene == 5)
        {
            Debug.Log("Explode a fábrica D:");
            explodirFabrica();
        }
        if (_indexCutscene == 6)
        {
            Debug.Log("Camera Move");
            _animator.SetTrigger("Move");
        }
        if (_indexCutscene == 7) // 
        {
            Debug.Log("Fica tudo de noite");
            _timeLater.gameObject.SetActive(true);
            globalLights[0].SetActive(false);
            globalLights[1].SetActive(true);
            _audioSource.clip = _clip[0]; // Chama o raio
        }
        if(_indexCutscene == 8)
        {   
            Debug.Log("Fica tudo de noite");
            _cameras[0].SetActive(false);
            _cameras[1].SetActive(true);
            _timeLater.gameObject.SetActive(false);
        }
        if ( _indexCutscene == 9)
        {
            _playerAnimations.PlayWakeUp();
        }
    }

    IEnumerator CallNextScene()
    {
        new WaitForSeconds(_tempoCutscenes);
        nextScene();
        yield return null;
    }

    // Update is called once per frame

    public void explodirFabrica()
    {
        _fabricaAnimator.SetTrigger("Explode");
    }
    

}
