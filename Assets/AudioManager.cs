using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource[] sfxAudioSources;
    [SerializeField] public AudioSource musicAudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        sfxAudioSources = GetComponentsInChildren<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public static void PlaySFX(SFX sfx)
    {
        foreach (AudioSource audioSource in instance.sfxAudioSources)
            if (!audioSource.isPlaying)
            {
                audioSource.clip = sfx.SFXClip;
                audioSource.volume = sfx.volume;
                audioSource.pitch = Random.Range(1 - sfx.pitchVariation, 1 + sfx.pitchVariation);
                audioSource.Play();
                return;
            }
    }
    public static void CrossfadeMusic(Music music, float duration)
    {
        instance.StartCoroutine(CrossfadeMusicCoroutine(music, duration));
    }
    private static IEnumerator CrossfadeMusicCoroutine(Music music, float duration)
    {
        float percent = 0;
        float startingVolume = instance.musicAudioSource.volume;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            instance.musicAudioSource.volume = Mathf.Lerp(startingVolume, 0, percent);
            yield return null;
        }
        instance.musicAudioSource.Stop();
        instance.musicAudioSource.clip = music.musicClip;
        instance.musicAudioSource.volume = music.volume;
        instance.musicAudioSource.Play();
        percent = 0;
        startingVolume = instance.musicAudioSource.volume;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            instance.musicAudioSource.volume = Mathf.Lerp(startingVolume, music.volume, percent);
            yield return null;
        }
    }
}
[System.Serializable]
public class SFX
{
    public AudioClip SFXClip;
    public float volume = 1f, pitchVariation = 0.1f;
    public AudioSource customAudioSource;

    public void Play()
    {
        if (customAudioSource == null) AudioManager.PlaySFX(this);
        else
        {
            customAudioSource.clip = SFXClip;
            customAudioSource.volume = volume;
            customAudioSource.pitch = Random.Range(1 - pitchVariation, 1 + pitchVariation);
            customAudioSource.Play();
        }
    }
}
[System.Serializable]
public class Music
{
    public AudioClip musicClip;
    public float volume = 1f, crossfadeDuration = 1f;
    public void Play() => AudioManager.CrossfadeMusic(this, crossfadeDuration);
}