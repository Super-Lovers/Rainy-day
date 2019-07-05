using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // Types of audio sources
    public List<AudioSource> BackgroundSources = new List<AudioSource>();
    public List<AudioSource> SoundSources = new List<AudioSource>();

    #region Audio volume controls
    private float _soundEffectsVolume;
    public float SoundEffectsVolume
    {
        get { return _soundEffectsVolume; }
        set
        {
            // We validate whether or not the volume is greater
            // than or less than it should be, to avoid exceptions.
            if (value > 100) { _soundEffectsVolume = 100; }
            else if (value < 0) { _soundEffectsVolume = 0; }
            else { _soundEffectsVolume = value; }
        }
    }

    private float _backgroundMusicVolume;
    public float BackgroundMusicVolume
    {
        get { return _backgroundMusicVolume; }
        set
        {
            if (value > 100) { _backgroundMusicVolume = 100; }
            else if (value < 0) { _backgroundMusicVolume = 0; }
            else { _backgroundMusicVolume = value; }
        }
    }
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// This function runs whenever the audio settings' volume parameter of
    /// the audio sources in the scenes is updated by the player.
    /// </summary>
    public void UpdateVolume()
    {
        foreach (AudioSource audioSource in SoundSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = SoundEffectsVolume / 100f;
            }
        }

        foreach (AudioSource audioSource in BackgroundSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = BackgroundMusicVolume / 100f;
            }
        }
    }

    #region Sound effects events
    public void MuteSoundEffects()
    {
        SoundEffectsVolume = 0;
        UpdateVolume();
    }

    public void IncreaseSoundVolume()
    {
        if (SoundEffectsVolume <= 90)
        {
            SoundEffectsVolume += 10;
        }

        UpdateVolume();
    }

    public void DecreaseSoundVolume()
    {
        if (SoundEffectsVolume >= 10)
        {
            SoundEffectsVolume -= 10;
        }
        
        UpdateVolume();
    }
    #endregion

    #region Background music events
    public void MuteBackgroundMusic()
    {
        BackgroundMusicVolume = 0;
        UpdateVolume();
    }

    public void IncreaseBackgroundVolume()
    {
        if (BackgroundMusicVolume <= 90)
        {
            BackgroundMusicVolume += 10;
        }
        
        UpdateVolume();
    }

    public void DecreaseBackgroundVolume()
    {
        if (BackgroundMusicVolume >= 10)
        {
            BackgroundMusicVolume -= 10;
        }
        
        UpdateVolume();
    }
    #endregion
}
