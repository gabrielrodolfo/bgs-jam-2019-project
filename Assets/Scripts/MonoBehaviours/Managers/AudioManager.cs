using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : Manager
{
    [SerializeField]
    private Transform audioSourcesParent;
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private List<AudioSource> sfxSources = new List<AudioSource>();

    private List<AudioSource> sfx3dSources = new List<AudioSource>();

    private AudioSource freeSFXSource
    {
        get 
        {
            AudioSource sfxs = sfxSources.SingleOrDefault(x => !x.isPlaying);

            if (sfxs == null)
            {
                GameObject sfxGo = new GameObject("SFX Source");
                sfxGo.transform.SetParent(audioSourcesParent);
                sfxs = sfxGo.AddComponent<AudioSource>();
                sfxSources.Add(sfxs);
            }

            return sfxs;
        }
    }

    private AudioSource freeSFX3DSource
    {
        get
        {
            AudioSource sfxs = sfx3dSources.SingleOrDefault(x => !x.isPlaying);

            if (sfxs == null)
            {
                GameObject go = new GameObject("3D SFX Source");
                sfxs = go.AddComponent<AudioSource>();
                sfxs.spatialize = true;
                sfxs.spatialBlend = 1f;
            }

            return sfxs;
        }
    }

    public void PlayBGM(AudioClip bgm)
    {
        bgmSource.Stop();
        bgmSource.clip = bgm;
        bgmSource.Play();
    }

    private void PlaySFX(AudioSource sfxs, AudioClip sfx)
    {
        sfxs.clip = sfx;
        sfxs.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        PlaySFX(freeSFXSource, sfx);
    }

    public void PlaySFX(AudioClip sfx, float volume)
    {
        AudioSource sfxs = freeSFXSource;
        sfxs.volume = volume;
        
        PlaySFX(sfxs, sfx);
    }

    public void PlaySFX(AudioClip sfx, float volume, float pitch)
    {
        AudioSource sfxs = freeSFXSource;
        sfxs.volume = volume;
        sfxs.pitch = pitch;
        
        PlaySFX(sfxs, sfx);
    }

    public void PlaySFXAt(AudioClip sfx, Vector3 position)
    {
        AudioSource sfxs = freeSFX3DSource;
        sfxs.transform.position = position;

        PlaySFX(sfxs, sfx);
    }

    public void PlaySFXAt(AudioClip sfx, Vector3 position, float volume)
    {
        AudioSource sfxs = freeSFX3DSource;
        sfxs.transform.position = position;
        sfxs.volume = volume;

        PlaySFX(sfxs, sfx);
    }

    public void PlaySFXAt(AudioClip sfx, Vector3 position, float volume, float pitch)
    {
        AudioSource sfxs = freeSFX3DSource;
        sfxs.transform.position = position;
        sfxs.volume = volume;
        sfxs.pitch = pitch;

        PlaySFX(sfxs, sfx);
    }
}
