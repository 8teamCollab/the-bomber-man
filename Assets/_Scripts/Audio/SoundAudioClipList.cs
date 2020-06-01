using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class SoundAudioClipList
{
    public SoundList soundList;
    public List<AudioClip> audioClips;

    public AudioMixerGroup output;

    public bool loop = false;
    public bool ignoreListenerPause = false;

    [Range(0.1f, 1f)] public float volume = 0.7f;
    [Range(0.1f, 2f)] public float pitch = 1f;

    [Range(0f, 0.5f)] public float volumeRandomness = 0f;
    [Range(0f, 0.5f)] public float pitchRandomness = 0f;

    [Range(0f, 15f)] public float maxDistance = 10f;
    [Range(0f, 1f)] public float spatialBlend = 1f;
}
