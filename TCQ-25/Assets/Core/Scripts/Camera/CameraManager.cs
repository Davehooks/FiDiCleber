using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;


public class CameraManager : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera[] _AllCameras;

    public static CameraManager instance;

    [Header("Controles pra velocidade em Y quando tiver caindo e subindo")]
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.25f;
    public float _fallSpeedYDampingChangeThresold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _leypYPanCoroutine;

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _FramingTransposer;

    private float _normYPanAmount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = new CameraManager();
        }

        for( int i =0;i< _AllCameras.Length;i++)
        {
            if( _AllCameras[i].enabled )
            {
                //setar a camera ativa
                _currentCamera = _AllCameras [i];

                //set the framing transposer( ?? O que é isso, Deus? D: )
                _FramingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        _normYPanAmount = _FramingTransposer.m_YDamping; // Apaga? O que isso faz meu deus
    }

    #region Lerp the Y Damping

    public void LerpYDaping(bool isPlayerFalling)
    {
        _leypYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerfalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = _FramingTransposer.m_YDamping;
        float endDampAmount = 0f;

        if (isPlayerfalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _normYPanAmount;
        }

        //Lerp the pan amount
        float elapsedTime = 0f;
        while(elapsedTime < _fallYPanTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elapsedTime/_fallYPanTime);
            _FramingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }


            IsLerpingYDamping = false;
    }

    #endregion


}