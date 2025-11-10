using UnityEngine;

public class CallSFX : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip[] AudioClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void CallSfx(int i)
    {
        AudioSource.clip = AudioClip[i];
        AudioSource.Play();
    }
    public void CallSfxAnimation(int i)
    {
        AudioSource.clip = AudioClip[i];
        AudioSource.Play();
    }
}
