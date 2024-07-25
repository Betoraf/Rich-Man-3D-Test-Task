using ButchersGames;
using Game.Manager;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Game.Tools;

namespace Game.LevelComponents
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private float _maxMoney;
        [SerializeField] private Canvas _main;
        [SerializeField] private Canvas _level;
        [SerializeField] private Canvas _victory;
        [SerializeField] private Canvas _defeat;
        [SerializeField] private Canvas _moneyBar;
        [SerializeField] private Image _moneyBarFill;
        [SerializeField] private float _fillSpeed = 0.5f;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private CreateSound _levelCompletedSound;
        [SerializeField] private CreateSound _levelDefeatSound;

        private Coroutine _fillCoroutine;

        private void OnEnable()
        {
            GameManager.StartGameEvent += StartGame;
            GameManager.PickUpEvent += UpdatePickUp;
        }

        private void OnDisable()
        {
            GameManager.StartGameEvent -= StartGame;
            GameManager.PickUpEvent -= UpdatePickUp;
        }

        private void StartGame()
        {
            _main.enabled = false;
            _level.enabled = true;
            _moneyBar.enabled = true;
            UpdatePickUp();
        }

        private void UpdatePickUp()
        {
            float targetFillAmount = GameManager.Instance.currentMoney / _maxMoney;
            _moneyText.text = GameManager.Instance.currentMoney.ToString();

            if (_fillCoroutine != null)
            {
                StopCoroutine(_fillCoroutine);
            }

            _fillCoroutine = StartCoroutine(SmoothFill(targetFillAmount));

            if (GameManager.Instance.currentMoney <= 0)
                LevelDefeat();
        }

        private IEnumerator SmoothFill(float targetFillAmount)
        {
            float initialFillAmount = _moneyBarFill.fillAmount;
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * _fillSpeed;
                _moneyBarFill.fillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, elapsedTime);
                yield return null;
            }

            _moneyBarFill.fillAmount = targetFillAmount;
        }

        public void LevelCompleted()
        {
            _level.enabled = false;
            _victory.enabled = true;
            _moneyBar.enabled = false;
            _levelCompletedSound.Create();
            GameManager.Instance.LevelCompleted(true);
        }

        public void LevelDefeat()
        {
            _level.enabled = false;
            _defeat.enabled = true;
            _moneyBar.enabled = false;
            _levelDefeatSound.Create();
            GameManager.Instance.LevelCompleted(false);
        }
        public void RestartLevel()
        {
            GameManager.Instance.currentMoney = 20;
            FindObjectOfType<LevelManager>().RestartLevel();
        }
    }
}
