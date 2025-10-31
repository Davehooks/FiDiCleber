using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class Morse : MonoBehaviour
{
    private float pressStart;
    private bool isPressing = false;

    private string currentMorse = "";
    public float inputThreshold = 0.3f;
    public float morseTimeout = 1f;
    private Coroutine timeout;

    private Dictionary<string, char> morseDictionary = new Dictionary<string, char>()
    {
        {".-", 'A'}, {"-...", 'B'}, {"-.-.", 'C'}, {"-..", 'D'},
        {".", 'E'}, {"..-.", 'F'}, {"--.", 'G'}, {"....", 'H'},
        {"..", 'I'},{".---", 'J'}, {"-.-", 'K'}, {".-..", 'L'},
        
        {"--", 'M'}, {"-.", 'N'}, {"---", 'O'}, {".--.", 'P'},
        {"--.-", 'Q'}, {".-.", 'R'}, {"...", 'S'}, {"-", 'T'},
        {"..-", 'U'}, {"...-", 'V'}, {".--", 'W'}, {"-..-", 'X'},
        {"-.--", 'Y'}, {"--..", 'Z'},
    };

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) 
        {
            return;
        }

        if (keyboard.kKey.wasPressedThisFrame)
        {
            pressStart = Time.time;
            isPressing = true;

            if(timeout != null)
            {
                StopCoroutine(timeout);
            }
            timeout = StartCoroutine(WaitForNoPress());
        }

        IEnumerator WaitForNoPress()
        {
            yield return new WaitForSeconds(morseTimeout);
            Debug.Log($"{morseTimeout} segundos sem apertar, limpando codigo");
            currentMorse = "";
        }
        if (keyboard.kKey.wasReleasedThisFrame && isPressing)
        {
            float pressDuration = Time.time - pressStart;

            if (pressDuration < inputThreshold)
            {
                currentMorse += ".";
                Debug.Log("Ponto");
            }
            else
            {
                currentMorse += "-";
                Debug.Log("Traço");
            }

            isPressing = false;
            Debug.Log("Código atual: " + currentMorse);
            
        }

        if(!isPressing)
        {

        }

        if (keyboard.lKey.wasPressedThisFrame)
        {
            StopCoroutine(timeout);
            if (morseDictionary.TryGetValue(currentMorse, out char letter))
            {

                Debug.Log("Letra: " + letter);
            }

            else
            {
                Debug.Log("Código inválido: " + currentMorse);
            }

            currentMorse = "";
        }
    }
}
