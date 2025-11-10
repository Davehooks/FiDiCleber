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
            Debug.Log(SFXSources.clip.ToString());
        }
    }

    public void PlayDamage(bool morto)
    {
        if (SFXSources != null)
        {
            if (!morto)
            {
                SFXSources.clip = SFXs[0];
                Debug.Log("Tocou dano");
            }
            if (morto)
            {
                SFXSources.clip = SFXs[1];
                Debug.Log("Tocou morte");
            }
            SFXSources.Play();

        }
    }
    public void PlayJump()
    {
        Debug.Log("Era pra tocar pulo");
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[2];
            SFXSources.Play();
            Debug.Log("Tocou pulo");
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

            Debug.Log("Tocou dash");
        }
    }
    public void PlayTrocarModo()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[5];
            SFXSources.Play();

            Debug.Log("Tocou TrocarModo");
        }
    }
    public void PlayErrouModo()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[6];
            SFXSources.Play();

            Debug.Log("Tocou ErrouModo");
        }
    }

    public void PlayBlock()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[7];
            SFXSources.Play();

            Debug.Log("Tocou Block");
        }
    }
    public void PlayStun()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[8];
            SFXSources.Play();

            Debug.Log("Tocou Stun");
        }
    }

    public void PlayReflect()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[9];
            SFXSources.Play();

            Debug.Log("Tocou Reflect");
        }
    }

    public void PlayMeleeAttack()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[10];
            SFXSources.Play();

            Debug.Log("Tocou MeleeAttack");
        }
    }

    public void PlayRangedAttack()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[11];
            SFXSources.Play();

            Debug.Log("Tocou RangedAttack");
        }
    }





}
