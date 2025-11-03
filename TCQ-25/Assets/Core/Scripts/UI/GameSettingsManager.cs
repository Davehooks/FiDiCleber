using UnityEngine;
using UnityEngine.Audio;

public class GameSettingsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameSettingsManager SettingsInstance;

    [Header("Configs")]
    [SerializeField] private AudioMixer fxMixer;
    [SerializeField] private AudioMixer music;
    [SerializeField] private float musicVolume;
    [SerializeField] private float fxVolume;
    private bool _isFullScreen;

    private void Awake()
    {
        if (SettingsInstance == null)
        {
            SettingsInstance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        LoadSettings();
    }
    private void LoadSettings()
    {
        _isFullScreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        fxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        SetFullscreen(_isFullScreen);
        SetMusicVolume(musicVolume);
        SetFxVolume(fxVolume);
    }
    public void SetFullscreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("Fullscreen",isFullScreen ? 1 : 0);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("mexeu no SetMusicVolume do GameSettingsManager");

        musicVolume = volume;
        music.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetFxVolume(float volume)
    {
        Debug.Log("mexeu no SetFXVolume do GameSettingsManager");

        fxVolume = volume;
        fxMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume",volume);
    }

    public float GetFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 0.5f);
    }

    public bool IsFullScreen()
    {
        return _isFullScreen;
    }


}
