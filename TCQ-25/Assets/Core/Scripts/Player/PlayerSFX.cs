using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] SFXs;
    [SerializeField] private AudioSource SFXSources;

    private void Start()
    {
        SFXSources = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SFXSources.clip != null)
        {
        }
    }

    public void PlayDamage(bool morto)
    {
        if (SFXSources != null)
        {
            if (!morto)
            {
                SFXSources.clip = SFXs[0];
            }
            if (morto)
            {
                SFXSources.clip = SFXs[1];
            }
            SFXSources.Play();

        }
    }
    public void PlayJump()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[2];
            SFXSources.Play();
        }
    }

    public void PlayDrop()
    {
        SFXSources.clip = SFXs[3];
        SFXSources.Play();

    }
    public void PlayDash()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[4];
            SFXSources.Play();

        }
    }
    public void PlayTrocarModo()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[5];
            SFXSources.Play();

        }
    }
    public void PlayErrouModo()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[6];
            SFXSources.Play();
        }
    }

    public void PlayBlock()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[7];
            SFXSources.Play();

        }
    }
    public void PlayStun()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[8];
            SFXSources.Play();
        }
    }

    public void PlayReflect()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[9];
            SFXSources.Play();

        }
    }

    public void PlayMeleeAttack()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[10];
            SFXSources.Play();

        }
    }

    public void PlayRangedAttack()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[11];
            SFXSources.Play();

        }
    }





}
