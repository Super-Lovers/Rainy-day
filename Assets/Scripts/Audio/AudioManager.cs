using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // Types of audio sources
    public List<AudioSource> background_sources = new List<AudioSource>();
    public List<AudioSource> sound_sources = new List<AudioSource>();

    private float previous_sound_effects_volume;
    [SerializeField]
    private float sound_effects_volume = 10;
    public float SoundEffectsVolume
    {
        get { return sound_effects_volume; }
        set
        {
            // We validate whether or not the volume is greater
            // than or less than it should be, to avoid exceptions.
            if (value > 100) { sound_effects_volume = 100; }
            else if (value < 0) { sound_effects_volume = 0; }
            else { sound_effects_volume = value; }
            UpdateVolume();
        }
    }

    private float previous_background_music_volume;
    [SerializeField]
    private float background_music_volume = 10;
    public float BackgroundMusicVolume
    {
        get { return background_music_volume; }
        set
        {
            if (value > 100) { background_music_volume = 100; }
            else if (value < 0) { background_music_volume = 0; }
            else { background_music_volume = value; }
            UpdateVolume();
        }
    }

    [NonSerialized]
    public AudioController audio_controller;

    private void Awake()
    {
        audio_controller = GetComponent<AudioController>();

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
        foreach (var audio_source in sound_sources)
        {
            if (audio_source != null)
            {
                audio_source.volume = SoundEffectsVolume / 100f;
            }
        }

        foreach (var audio_source in background_sources)
        {
            if (audio_source != null)
            {
                audio_source.volume = BackgroundMusicVolume / 100f;
            }
        }

        AudioManager.Instance.audio_controller.PlaySound("Volume Update");
    }

    public void MuteSoundEffects()
    {
        previous_sound_effects_volume = SoundEffectsVolume;
        SoundEffectsVolume = 0;
    }

    public void UnmuteSoundEffects() {
        SoundEffectsVolume = previous_sound_effects_volume;
    }

    public void IncreaseSoundVolume()
    {
        if (SoundEffectsVolume <= 90)
        {
            SoundEffectsVolume += 10;
        }
    }

    public void DecreaseSoundVolume()
    {
        if (SoundEffectsVolume >= 10)
        {
            SoundEffectsVolume -= 10;
        }
    }

    public void MuteBackgroundMusic()
    {
        previous_background_music_volume = BackgroundMusicVolume;
        BackgroundMusicVolume = 0;
    }

    public void UnmuteBackgroundMusic() {
        BackgroundMusicVolume = previous_background_music_volume;
    }

    public void IncreaseBackgroundVolume()
    {
        if (BackgroundMusicVolume <= 90)
        {
            BackgroundMusicVolume += 10;
        }
    }

    public void DecreaseBackgroundVolume()
    {
        if (BackgroundMusicVolume >= 10)
        {
            BackgroundMusicVolume -= 10;
        }
    }
}
