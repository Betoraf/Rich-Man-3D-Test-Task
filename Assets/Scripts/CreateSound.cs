using System.Collections;
using UnityEngine;

namespace Game.Tools
{
    public class CreateSound : MonoBehaviour
    {
        public SoundObject sound;
        public float delay;
        public float coolDown;

        private float _coolDown;

        private const float DISTANCE = 50f;

        public void Create()
        {
            if (_coolDown <= 0)
            {
                _coolDown = coolDown;
                if (delay != 0)
                    StartCoroutine(Delay());
                else
                    CreateSoundAfterDelay();
            }
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(delay);
            CreateSoundAfterDelay();
        }

        private void CreateSoundAfterDelay()
        {
            GameObject go = new();
            go.transform.position = transform.position;
            int r = Random.Range(0, sound.sounds.Length);
            var clip = sound.sounds[r];
            go.name = clip.name;
            var audioSource = go.AddComponent<AudioSource>();

            audioSource.clip = clip;
            audioSource.pitch = Random.Range(sound.minPitch, sound.maxPitch);
            audioSource.spatialBlend = sound.is3d;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.maxDistance = DISTANCE;
            audioSource.outputAudioMixerGroup = sound.mixer;

            audioSource.Play();

            go.AddComponent<DestroyAfterTime>().time = clip.length + 0.1f;
            DontDestroyOnLoad(go);
        }

        private void Update()
        {
            _coolDown -= Time.deltaTime;
        }
    }
}
