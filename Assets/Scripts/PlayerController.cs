using UnityEngine;
using UnityEngine.EventSystems;
using Game.Manager;

namespace Game.PlayerComponents
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStatuses playerStatuses;
        [SerializeField] private float _forwardSpeed = 10f;
        [SerializeField] private float _shiftBorders = 3f;
        [SerializeField] private Camera _camera;
        [SerializeField] private Rigidbody _rigidbody;

        private bool _gameStarted;
        private float _shift;
        private float _nextShift;
        private float _currentPosition;
        private float _previousPosition;
        private bool _isPressed;

        private void OnEnable()
        {
            GameManager.StartGameEvent += StartGame;
            GameManager.LevelCompletedEvent += LevelCompleted;
            GameManager.PickUpEvent += UpdatePickUp;
        }

        private void OnDisable()
        {
            GameManager.StartGameEvent -= StartGame;
            GameManager.LevelCompletedEvent -= LevelCompleted;
            GameManager.PickUpEvent -= UpdatePickUp;
        }

        private void StartGame()
        {
            playerStatuses.StartGame();
            _gameStarted = true;
        }

        private void Start()
        {
            playerStatuses.Start();
        }

        private void Update()
        {
            HandleMouseInput();
        }

        private void FixedUpdate()
        {
            if (_gameStarted)
            {
                MoveForward();
            }
        }

        private void HandleMouseInput()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(0))
            {
                BeginTouch();
            }
            else if (Input.GetMouseButton(0) && _isPressed)
            {
                UpdateTouch();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndTouch();
            }
        }
        private void BeginTouch()
        {
            _currentPosition = _previousPosition = GetMouseXPosition();
            _isPressed = true;
            GameManager.Instance.TryStartGame();
        }

        private void UpdateTouch()
        {
            _currentPosition = GetMouseXPosition();
            var shift = _currentPosition - _previousPosition;
            MoveToSside(shift);
            _previousPosition = _currentPosition;
        }

        private void EndTouch()
        {
            _isPressed = false;
        }

        private float GetMouseXPosition()
        {
            var mousePosition = Input.mousePosition;
            var mouseX = _camera.ScreenToViewportPoint(mousePosition).x;
            return (mouseX - 0.5f) * _shiftBorders;
        }

        private void MoveForward()
        {
            var velocity = Vector3.forward * _forwardSpeed;
            velocity.x = _shift / Time.fixedDeltaTime;
            _rigidbody.velocity = velocity;
            _shift = _nextShift;
            _nextShift = 0;
        }

        public void MoveToSside(float shift)
        {
            _nextShift += shift;
        }

        public void UpdatePickUp()
        {
            playerStatuses.CanChangeStatus();
        }

        public void LevelCompleted(bool _bool)
        {
            _gameStarted = false;
            _rigidbody.velocity = Vector3.zero;
            if (_bool)
                playerStatuses.LevelCompleted();
            else
                playerStatuses.LevelDefeat();
        }
    }
}
