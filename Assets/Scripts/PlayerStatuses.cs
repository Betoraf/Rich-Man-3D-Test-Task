using Game.Manager;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerComponents
{
    [Serializable]
    public class PlayerStatuses
    {
        [SerializeField] private Status[] _statuses;
        [SerializeField] private Animator _animator;
        [SerializeField] private UnityEvent _changeStatusEvents;

        private readonly string _spinKey = "Spin";
        private readonly string _walkKey = "Walk";
        private readonly string _upsetKey = "Upset";
        private readonly string _danceKey = "Dance";

        private Skin _activeSkin;

        public void Start()
        {
            _activeSkin = _statuses[1].skin;
            CanChangeStatus();
        }

        public void StartGame()
        {
            _animator.SetTrigger(_walkKey);
        }

        public void CanChangeStatus()
        {
            float currentMoney = GameManager.Instance.currentMoney;

            Status? activeStatus = null;
            foreach (var status in _statuses)
            {
                if (currentMoney >= status.money)
                {
                    if (activeStatus == null || status.money > activeStatus.Value.money)
                    {
                        activeStatus = status;
                    }
                }
            }

            if (activeStatus != null && activeStatus.Value.skin != _activeSkin)
            {
                if (_activeSkin != null)
                {
                    _activeSkin.Activate(false);
                }

                _animator.SetTrigger(_spinKey);
                activeStatus.Value.skin.Activate(true);
                _activeSkin = activeStatus.Value.skin;
                _changeStatusEvents?.Invoke();
            }
        }

        public void LevelCompleted()
        {
            _animator.SetTrigger(_danceKey);
        }

        public void LevelDefeat()
        {
            _animator.SetTrigger(_upsetKey);
        }
    }
}