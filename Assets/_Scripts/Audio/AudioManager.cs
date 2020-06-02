/*
 * Copyright(C) 2020 Artyom Bezmenov (8nhuman8)

 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * at your option) any later version.

 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see<https://www.gnu.org/licenses/>.
 */


using System.Collections.Generic;
using UnityEngine;


public class AudioManager : SingletonBehaviour<AudioManager>
{
    #region Variables

    [SerializeField] private List<SoundAudioClip> sounds;
    [SerializeField] private List<SoundAudioClipList> similarSounds;

    #endregion Variables


    public void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject($"Sound ({sound})");

            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound, audioSource, out bool loop);
            audioSource.Play();

            if (!loop)
            {
                Destroy(soundGameObject, audioSource.clip.length);
            }
        }
    }
    public void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject($"Sound ({sound})");
            soundGameObject.transform.position = position;

            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound, audioSource, out _);
            audioSource.Play();

            Destroy(soundGameObject, audioSource.clip.length);
        }
    }
    public void PlaySound(SoundList soundList, Vector3 position)
    {
        GameObject soundGameObject = new GameObject($"Sound ({soundList})");
        soundGameObject.transform.position = position;

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(soundList, audioSource, out _);
        audioSource.Play();

        Destroy(soundGameObject, audioSource.clip.length);
    }


    private bool CanPlaySound(Sound sound) //https://www.youtube.com/watch?v=QL29aTa7J5Q
    {
        switch (sound)
        {
            default:
                return true;
        }
    }


    private AudioClip GetAudioClip(Sound sound, AudioSource audioSource, out bool loop)
    {
        foreach (SoundAudioClip soundAudioClip in sounds)
        {
            if (soundAudioClip.sound == sound)
            {
                audioSource.outputAudioMixerGroup = soundAudioClip.output;

                audioSource.dopplerLevel = 0f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;

                audioSource.loop = soundAudioClip.loop;
                loop = soundAudioClip.loop;

                audioSource.ignoreListenerPause = soundAudioClip.ignoreListenerPause;

                float volumeRandomness = Random.Range(-soundAudioClip.volumeRandomness / 2f, soundAudioClip.volumeRandomness / 2f);
                audioSource.volume = soundAudioClip.volume * (1f + volumeRandomness);

                float pitchRandomness = Random.Range(-soundAudioClip.pitchRandomness / 2f, soundAudioClip.pitchRandomness / 2f);
                audioSource.pitch = soundAudioClip.pitch * (1f + pitchRandomness);

                audioSource.maxDistance = soundAudioClip.maxDistance;
                audioSource.spatialBlend = soundAudioClip.spatialBlend;

                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError($"Sound {sound} not found");
        loop = false;
        return null;
    }
    private AudioClip GetAudioClip(SoundList soundList, AudioSource audioSource, out bool loop)
    {
        foreach (SoundAudioClipList soundAudioClipList in similarSounds)
        {
            if (soundAudioClipList.soundList == soundList)
            {
                int randomIndex = Random.Range(0, soundAudioClipList.audioClips.Count);
                AudioClip randomAudioClip = soundAudioClipList.audioClips[randomIndex];

                audioSource.outputAudioMixerGroup = soundAudioClipList.output;

                audioSource.dopplerLevel = 0f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;

                audioSource.loop = soundAudioClipList.loop;
                loop = soundAudioClipList.loop;

                audioSource.ignoreListenerPause = soundAudioClipList.ignoreListenerPause;

                float volumeRandomness = Random.Range(-soundAudioClipList.volumeRandomness / 2f, soundAudioClipList.volumeRandomness / 2f);
                audioSource.volume = soundAudioClipList.volume * (1f + volumeRandomness);

                float pitchRandomness = Random.Range(-soundAudioClipList.pitchRandomness / 2f, soundAudioClipList.pitchRandomness / 2f);
                audioSource.pitch = soundAudioClipList.pitch * (1f + pitchRandomness);

                audioSource.maxDistance = soundAudioClipList.maxDistance;
                audioSource.spatialBlend = soundAudioClipList.spatialBlend;

                return randomAudioClip;
            }
        }

        Debug.LogError($"Sound list {soundList} not found");
        loop = false;
        return null;
    }
}
