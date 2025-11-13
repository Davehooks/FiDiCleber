using UnityEngine;

public class SetCameraPause : MonoBehaviour
{
    void Start()
    {
        SetPauseCamera.instance.SetCamera();
        Debug.Log($"Cuzinho + {this.name}");
    }

}
