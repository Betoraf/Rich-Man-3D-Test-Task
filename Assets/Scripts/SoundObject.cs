using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Tools
{
    [CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/SoundObject", order = 1)]
    public class SoundObject : ScriptableObject
    {
        public AudioClip[] sounds;
        [Range(0, 1)] public float playbackChance = 1;
        [Range(0, 1)] public float volume = 1;
        [Range(-3, 3)] public float minPitch = 1;
        [Range(-3, 3)] public float maxPitch = 1;
        [Range(0, 1)] public float is3d;
        public AudioMixerGroup mixer;
    }
}