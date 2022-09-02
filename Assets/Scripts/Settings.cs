using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetGameVolume(float volume)
    {
        mixer.SetFloat("GameVolume", volume);
    }

    public void SetGraphics(int val)
    {
        QualitySettings.SetQualityLevel(val);
    }
}
