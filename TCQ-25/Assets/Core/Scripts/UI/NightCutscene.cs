using UnityEngine;

public class NightCutscene : MonoBehaviour
{

    [SerializeField] private GameObject luzDia;
    void Awake()
    {
        luzDia.SetActive(false);
    }

}
