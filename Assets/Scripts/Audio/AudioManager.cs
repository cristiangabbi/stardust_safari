using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    public List<Sound> ambientSounds;
    Sound currentAmbient;


    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //load sound
        foreach (var element in sounds)
        {
            element.source = gameObject.AddComponent<AudioSource>();

            element.source.clip = element.clip;
            element.source.volume = element.volume;
            element.source.pitch = element.pitch;
            element.source.loop = element.loop;
        }

        //load ambientSound
        foreach (var element in ambientSounds)
        {
            element.source = gameObject.AddComponent<AudioSource>();

            element.source.clip = element.clip;
            element.source.volume = element.volume;
            element.source.pitch = element.pitch;
            element.source.loop = element.loop;
        }
    }

    private void Start()
    {
        PlayNextAmbientSound();

        InvokeRepeating("PlayNextAmbientSound", 120f, 60f);
    }


    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);

        //check that sound exists
        if(s != null)
        {
            s.source.Play();
        }
    }

    public void PlayNextAmbientSound()
    {
        if (currentAmbient == null || !currentAmbient.source.isPlaying)
        {
            currentAmbient = ambientSounds[0];
            currentAmbient.source.Play();

            //put music just played at the end of the queue
            ambientSounds.RemoveAt(0);
            ambientSounds.Insert(ambientSounds.Count, currentAmbient);
        }
    }

    public bool IsPlayingAmbient()
    {
        if (currentAmbient != null && currentAmbient.source.isPlaying)
            return true;
        else
            return false;
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);

        if (s != null && s.source.isPlaying)
            return true;
        else
            return false;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);

        //check that sound exists
        if (s != null)
        {
            s.source.Stop();
        }
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);

        //check that sound exists
        if (s != null)
        {
            s.source.Pause();
        }
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);

        //check that sound exists
        if (s != null)
        {
            s.source.UnPause();
        }
    }

    public void SetSongVolume(string name, float newVol)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);

        //check that sound exists
        if (s != null)
        {
            s.source.volume = newVol;
        }
    }
}
