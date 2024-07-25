using UnityEngine;
using UnityEngine.Events;

namespace Game.Environment
{
    public class FinishZone : MonoBehaviour
    {
        [SerializeField] private UnityEvent _events;

        private void OnTriggerEnter(Collider other)
        {
            _events?.Invoke();
        }
    }
}