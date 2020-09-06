using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private List<SoundFile> _soundEffects = new List<SoundFile>();
    public AudioType AudioType;

    AudioManager _audioManager;
    AudioSource _audioSource;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
        _audioSource = GetComponent<AudioSource>();
        List<AudioSource> soundSources = _audioManager.SoundSources;
        List<AudioSource> backgroundSources = _audioManager.BackgroundSources;

        if (AudioType == AudioType.Sound && soundSources.Contains(_audioSource) == false)
        {
            soundSources.Add(_audioSource);
            _audioSource.volume = _audioManager.SoundEffectsVolume / 100f;
        }
        else if (AudioType == AudioType.Background && backgroundSources.Contains(_audioSource) == false)
        {
            backgroundSources.Add(_audioSource);
            _audioSource.volume = _audioManager.BackgroundMusicVolume / 100f;
        }
    }

    public void PlaySound(string soundName)
    {
        bool isSoundFileFound = false;
        foreach (SoundFile soundFile in _soundEffects) {
            if (soundFile.Name == soundName && _audioSource.isPlaying == false)
            {
                isSoundFileFound = true;
                _audioSource.PlayOneShot(soundFile.AudioClip);
                break;
            }
        }

        if (isSoundFileFound == false)
        {
            Debug.Log($"The sound file <color=#ff0000>{soundName}</color> is not valid!");
        }
    }

    public void PlayLoopedSound(string soundName) {
        bool isSoundFileFound = false;
        foreach (SoundFile soundFile in _soundEffects) {
            if (soundFile.Name == soundName && _audioSource.isPlaying == false) {
                isSoundFileFound = true;

                _audioSource.clip = soundFile.AudioClip;
                _audioSource.Play();
                break;
            }
        }

        if (isSoundFileFound == false) {
            Debug.Log($"The sound file <color=#ff0000>{soundName}</color> is not valid!");
        }
    }

    public void Stop() {
        _audioSource.Stop();
    }
}
