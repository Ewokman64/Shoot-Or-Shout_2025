using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip zombieDeath;
    public AudioClip shoot;
    public AudioClip shout;
    public AudioClip dashShot;
    public AudioClip dashShotReload;
    public AudioSource AudioSource;

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

    public IEnumerator PlayDashShotReload()
    {
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayOneShot(dashShotReload, 1.0f);
    }

    public void PlayDashShot()
    {
        AudioSource.PlayOneShot(dashShot, 1.0f);
    }
}
