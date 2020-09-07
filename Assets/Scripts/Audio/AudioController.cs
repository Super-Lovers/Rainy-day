using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private List<SoundFile> sound_effects = new List<SoundFile>();
    public AudioType audio_type;

    private AudioManager audio_manager;
    private AudioSource audio_source;

    private void Start()
    {
        audio_manager = AudioManager.Instance;
        audio_source = GetComponent<AudioSource>();

        if (audio_manager != null) {
            var sound_sources = audio_manager.sound_sources;
            var background_sources = audio_manager.background_sources;

            if (audio_type == AudioType.Sound && sound_sources.Contains(audio_source) == false) {
                sound_sources.Add(audio_source);
                audio_source.volume = audio_manager.SoundEffectsVolume / 100f;
            } else if (audio_type == AudioType.Background && background_sources.Contains(audio_source) == false) {
                background_sources.Add(audio_source);
                audio_source.volume = audio_manager.BackgroundMusicVolume / 100f;
            }
        }
    }

    public void PlaySound(string sound_name)
    {
        var is_sound_file_found = false;
        foreach (var sound_file in sound_effects) {
            if (sound_file.name == sound_name && audio_source.isPlaying == false)
            {
                is_sound_file_found = true;
                audio_source.PlayOneShot(sound_file.audio_clip);
                break;
            }
        }

        if (PlayerController.Instance.debug_mode == true && is_sound_file_found == false) {
            Debug.Log($"The sound file <color=#ff0000>{sound_name}</color> is not valid!");
        }
    }

    public void PlayLoopedSound(string sound_name) {
        var is_sound_file_found = false;
        foreach (var sound_file in sound_effects) {
            if (sound_file.name == sound_name && audio_source.isPlaying == false) {
                is_sound_file_found = true;

                audio_source.clip = sound_file.audio_clip;
                audio_source.Play();
                break;
            }
        }

        if (PlayerController.Instance.debug_mode == true && is_sound_file_found == false) {
            Debug.Log($"The sound file <color=#ff0000>{sound_name}</color> is not valid!");
        }
    }

    public void Stop() {
        audio_source.Stop();
    }
}
