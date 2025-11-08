using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] SFXs;
    private AudioSource SFXSources;

    private void Start()
    {
        SFXSources = GetComponent<AudioSource>();
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
        }
    }
    public void PlayJump()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[2];
            Debug.Log("Tocou pulo");
        }
    }
    public void PlayDash()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[3];
            Debug.Log("Tocou dash");
        }
    }
    public void PlayTrocarModo()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[4];
            Debug.Log("Tocou TrocarModo");
        }
    }
    public void PlayErrouModo()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[5];
            Debug.Log("Tocou ErrouModo");
        }
    }

    public void PlayBlock()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[6];
            Debug.Log("Tocou Block");
        }
    }

    public void PlayStun()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[7];
            Debug.Log("Tocou Stun");
        }
    }

    public void PlayReflect()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[8];
            Debug.Log("Tocou Reflect");
        }
    }

    public void PlayMeleeAttack()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[9];
            Debug.Log("Tocou MeleeAttack");
        }
    }

    public void PlayRangedAttack()
    {
        if (SFXSources != null)
        {
            SFXSources.clip = SFXs[10];
            Debug.Log("Tocou RangedAttack");
        }
    }





}
