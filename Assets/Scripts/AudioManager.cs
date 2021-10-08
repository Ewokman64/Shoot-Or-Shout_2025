using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip zombieDeath;
    public AudioClip shoot;
    public AudioClip shout;
    public AudioClip reload;
    public AudioSource AudioSource;

    public void PlayZombieDeath()
    {
        AudioSource.PlayOneShot(zombieDeath, 1.0f);
    }
    public void PlayShoot()
    {
        AudioSource.PlayOneShot(shoot, 1.0f);
    }

    public void PlayShout()
    {
        AudioSource.PlayOneShot(shout, 1.0f);
    }

    public void PlayReload()
    {
        AudioSource.PlayOneShot(reload, 1.0f);
    }
}
