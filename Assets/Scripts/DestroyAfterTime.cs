using System.Collections;
using UnityEngine;

namespace Game.Tools
{
    public class DestroyAfterTime : MonoBehaviour
    {
        public float time = 1;
        public bool playOnAwake = true;

        private void Start()
        {
            if (playOnAwake)
                Play();
        }

        public void Play()
        {
            StartCoroutine(Timer());
        }

        public void Play(float time)
        {
            this.time = time;
            Play();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(time);

            Destroy(gameObject);
        }
    }
}