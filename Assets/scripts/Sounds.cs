using System.Collections;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    private AudioSource musicSource;
    private AudioSource sfxSource;
    public AudioClip hurtSound;
    public AudioClip doorSound;
    public AudioClip chestSound;
    public AudioClip chest2Sound;
    public AudioClip shrinkSound;
    public AudioClip startSound;
    public AudioClip dieSound;
    public AudioClip[] bulletSounds;
    private int rand;
    public AudioClip Music;
    public AudioClip bossMusic;

    public bool start = false;

    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        if(start)
        {
            PlayStartSound();
        }
        else
        {
            PlayMusic();
        }
    }

    public void playBulletSound()
    {
        rand = Random.Range(0, bulletSounds.Length);
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(bulletSounds[rand], .35f);
    }
    public void playHurtSound()
    {
        sfxSource.pitch = Random.Range(0.7f, 1.3f);
        sfxSource.PlayOneShot(hurtSound, .35f);
    }

    public void playDoorSound()
    {
        sfxSource.pitch = Random.Range(0.7f, 1.3f);
        sfxSource.PlayOneShot(doorSound, .55f);
    }

    public void playChestSound()
    {
        sfxSource.pitch = Random.Range(0.7f, 1.3f);
        sfxSource.PlayOneShot(chestSound, .55f);
    }

    public void playChest2Sound()
    {
        sfxSource.pitch = Random.Range(0.7f, 1.3f);
        sfxSource.PlayOneShot(chest2Sound, .55f);
    }

    public void playShrinkSound()
    {
        sfxSource.pitch = Random.Range(0.7f, 1.3f);
        sfxSource.PlayOneShot(shrinkSound, .55f);
    }
    public void PlayStartSound()
    {
        sfxSource.PlayOneShot(startSound, .55f);
    }
    public void PlayDieSound()
    {
        sfxSource.pitch = 1;
        sfxSource.PlayOneShot(dieSound, .55f);
    }


    private void PlayMusic()
    {
        musicSource.clip = Music;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlayBossMusic()
    {
        musicSource.clip = bossMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}
