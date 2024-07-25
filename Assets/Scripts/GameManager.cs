using System;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static Action StartGameEvent;
        public static Action<bool> LevelCompletedEvent;
        public static Action PickUpEvent;
        [SerializeField] public float currentMoney;

        private bool gameStarted;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        public void TryStartGame()
        {
            if (!gameStarted)
            {
                gameStarted = true;
                StartGameEvent?.Invoke();
            }
        }

        public void PickUp(float addMoney)
        {
            if ((currentMoney += addMoney) >= 0)
                currentMoney += addMoney;
            else
                currentMoney = 0;
            PickUpEvent?.Invoke();
        }

        public void LevelCompleted(bool _bool)
        {
            if (gameStarted)
            {
                gameStarted = false;
                LevelCompletedEvent?.Invoke(_bool);
            }
        }
    }
}