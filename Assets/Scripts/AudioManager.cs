using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource AudioSource;

    public AudioClip zombieDeath;

    public AudioClip shoot;
    public AudioClip shout;

    public AudioClip dashSound;
    public AudioClip dashShot;
    public AudioClip dashShotReload;
    public AudioClip d_shot_Explosion;

    public AudioClip dashShout;

    public AudioClip shieldOn;
    public AudioClip shieldOff;

    public AudioClip damaged;
    public void PlayZombieDeath()
    {
        AudioSource.PlayOneShot(zombieDeath, 0.3f);
    }
    public void PlayShoot()
    {
        AudioSource.PlayOneShot(shoot, 0.5f);
    }

    public void PlayShout()
    {
        AudioSource.PlayOneShot(shout, 1.0f);
    }

    public void PlayDashSound()
    {
        AudioSource.PlayOneShot(dashSound, 1.0f);
    }
    public IEnumerator PlayDashShotReload()
    {
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayOneShot(dashShotReload, 1.0f);
    }

    public void PlayDashShot()
    {
        AudioSource.PlayOneShot(dashShot, 1.0f);
    }

    public void PlayShotExplosion()
    {
        AudioSource.PlayOneShot(d_shot_Explosion, 1.0f);
    }

    public void PlayDashShout()
    {
        AudioSource.PlayOneShot(dashShout, 1.0f);
    }

    public void PlayShieldOn()
    {
        AudioSource.PlayOneShot(shieldOn, 1.5f);
    }

    public void PlayShieldOff()
    {
        AudioSource.PlayOneShot(shieldOff, 1.0f);
    }

    public void PlayDamaged()
    {
        AudioSource.PlayOneShot(damaged, 1.0f);
    }
}
