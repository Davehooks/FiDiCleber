using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _configPanel;
    [SerializeField] private Button[] _buttons;
    private int _indexButton;
    void Start()
    {
        if (_buttons == null)
        {
            Debug.Log("Não tem botão no MenuSript");
        }


    }
    void Update()
    {
        foreach (Button button in _buttons)
        {
            button.image.color = Color.white;
        }
        _buttons[_indexButton].image.color = Color.yellow;
    }

    public void IndexPlus(InputAction.CallbackContext context)
    {
        if (!_configPanel.activeInHierarchy)
        {
        _indexButton = (_indexButton + 1 + _buttons.Length) % _buttons.Length;
        }
    }
    public void IndexMinus(InputAction.CallbackContext context)
    {
        if (!_configPanel.activeInHierarchy)
        {
        _indexButton = (_indexButton - 1 + _buttons.Length) % _buttons.Length;
        }
    }
    public void IndexSelect(InputAction.CallbackContext context)
    {
        if (!_configPanel.activeInHierarchy)
        {
            switch (_indexButton)
            {
                case 0: StartGame();  // TODO mudar o nome disso aqui se for mudar o nome da cena
                    break;
                case 1: OpenConfigPanel();
                    break;
                case 2: QuitGame();
                    break;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenConfigPanel()
    {
        _configPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void ClosePanel(InputAction.CallbackContext context)
    {

        if(_configPanel.activeInHierarchy)
        {
        UIManager.UImanagerInstance.ClosePanel();
        }
    }

}
