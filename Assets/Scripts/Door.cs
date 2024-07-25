using UnityEngine;
using UnityEngine.Events;

namespace Game.Environment
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private UnityEvent events;

        private readonly string _openKey = "Open";

        private void OnTriggerEnter(Collider other)
        {
            animator.SetTrigger(_openKey);
            events?.Invoke();
        }
    }
}
