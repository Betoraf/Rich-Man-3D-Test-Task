using UnityEngine;
using UnityEngine.Events;

namespace Game.Environment
{
    public class FlagZone : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private UnityEvent _events;

        private readonly string _upKey = "Up";

        private void OnTriggerEnter(Collider other)
        {
            _animator.SetTrigger(_upKey);
            _events?.Invoke();
        }
    }

}