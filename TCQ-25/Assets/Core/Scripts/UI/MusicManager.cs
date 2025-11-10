using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public enum MusicState {Menu, Exploration, Battle}

    [Header("Referências")]
    public MusicState currentMusicState = MusicState.Menu;




    public static MusicManager MusicInstance; // fazer com que possa ser chamado independente do lugar
    [Header("Configuracoes")]
    public AudioClip[] tracks;

    private AudioSource musicSource;

    private void Awake()
    {
        if (MusicInstance == null)
        {
            MusicInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        switch (SceneManager.GetActiveScene().ToString())
        {
            case "SampleScene":
                currentMusicState = MusicState.Exploration;
                return;
            case "MenuScene":
                currentMusicState=MusicState.Menu;
                return;
            case "Boss":
                currentMusicState=MusicState.Battle;
                return;
        }
        PlayTrack(currentMusicState);
    }
    


    public void PlayTrack(MusicState state)
    {
        int trackIndex=-1;

        switch (state)
        {
            case MusicState.Menu:
                trackIndex = 0;
                break;
            case MusicState.Exploration:
                trackIndex = 1;
                break;
            case MusicState.Battle:
                trackIndex = 2;
                break;
            default:
                Debug.Log("Nao existe esse state");
                break;
        }
        musicSource.clip = tracks[trackIndex];
        musicSource.Play();
    }
}
