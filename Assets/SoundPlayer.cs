using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private SFX[] sfxs;
    [SerializeField] private Music[] musics;
    [SerializeField] private bool playMusic0OnStart = false;
    public void PlaySFX(int index) => sfxs[index].Play();
    public void PlayMusic(int index) => musics[index].Play();

    private void Start()
    {
        if (playMusic0OnStart && musics.Length > 0) PlayMusic(0);
    }
}
