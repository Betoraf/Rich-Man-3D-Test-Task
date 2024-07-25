using Game.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PickUp
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] private float addMoney = 1;
        [SerializeField] private UnityEvent _pickUpEvents;

        private void OnTriggerEnter(Collider other)
        {
            GameManager.Instance.PickUp(addMoney);
            _pickUpEvents?.Invoke();
            Destroy(gameObject);
        }
    }
}