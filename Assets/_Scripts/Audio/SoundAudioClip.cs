/*
 * Copyright(C) 2020 Artyom Bezmenov (8nhuman8)

 * This file is part of The Bomber Man.

 * The Bomber Man is free software: you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with this The Bomber Man. If not,
 * see <https://www.gnu.org/licenses/>.
 */


using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class SoundAudioClip
{
    public Sound sound;
    public AudioClip audioClip;

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
