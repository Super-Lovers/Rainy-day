using System.Collections.Generic;
using UnityEngine;

public class Stereo : MonoBehaviour
{
    public List<AudioClip> music_clips = new List<AudioClip>();
    private List<AudioClip> played_clips = new List<AudioClip>();
    private AudioClip current_clip;

    private AudioSource audio_source;

    private void Awake() {
        audio_source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audio_source.clip = GetRandomMusic();
        audio_source.Play();
    }

    private AudioClip GetRandomMusic()
    {
        var new_clip = music_clips[Random.Range(0, music_clips.Count)];
        while (played_clips.Contains(new_clip))
        {
            new_clip = music_clips[Random.Range(0, music_clips.Count)];
        }
        played_clips.Add(current_clip);
        current_clip = new_clip;

        if (played_clips.Count == music_clips.Count)
        {
            played_clips.Clear();
            if (PlayerController.Instance.debug_mode == true) {
                Debug.Log($"Stereo playlist is <color=#ffffff>reset</color>");
            }
        }

        if (PlayerController.Instance.debug_mode == true) {
            Debug.Log($"Now playing: <color=#008080ff>{new_clip.name}</color>");
        }

        return current_clip;
    }
}
