using System.Collections.Generic;
using UnityEngine;

public class Stereo : MonoBehaviour
{
    public List<AudioClip> MusicClips = new List<AudioClip>();
    private List<AudioClip> _playedClips = new List<AudioClip>();
    private AudioClip _currentClip;

    #region Components
    AudioSource _audioSource;
    #endregion

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _audioSource.clip = GetRandomMusic();
        _audioSource.Play();
    }

    private AudioClip GetRandomMusic()
    {
        AudioClip newClip = MusicClips[Random.Range(0, MusicClips.Count)];
        while (_playedClips.Contains(newClip))
        {
            newClip = MusicClips[Random.Range(0, MusicClips.Count)];
        }
        _playedClips.Add(_currentClip);
        _currentClip = newClip;

        if (_playedClips.Count == MusicClips.Count)
        {
            _playedClips.Clear();
            Debug.Log($"Stereo playlist is <color=#ffffff>reset</color>");
        }

        Debug.Log($"Now playing: <color=#008080ff>{newClip.name}</color>");

        return _currentClip;
    }
}
