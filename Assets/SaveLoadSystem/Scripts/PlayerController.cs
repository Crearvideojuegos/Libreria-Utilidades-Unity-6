using UnityEngine;
using UnityEngine.AI;

namespace SpaceSaveLoadSystem
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance; //Singleton
        private InAcSaveLoadSystem _inAcSaveLoadSystem; //Input System
        private CharacterController _characterController;
        private float _xRotation;
        [SerializeField] private Transform _camTrans;
        private Vector2 _moveInput; //WASD Movement
        private Vector3 _moveData;
        private Vector2 _lookInput; //Mouse Look Movement
        [SerializeField] private float _lookSensitivityX = 0.1f;
        [SerializeField] private float _lookSensitivityY = 0.1f;
        [SerializeField] private GameObject _panelPause;
        private bool _gamePaused;
        private float _timePlayed;
        private int _coins;
        private bool _canSaved;

        private void Awake() 
        {
            Instance = this;
            _inAcSaveLoadSystem = new InAcSaveLoadSystem();
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            _gamePaused = false;
            _coins = 0;
            _canSaved = false;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            _inAcSaveLoadSystem.Enable();
            _inAcSaveLoadSystem.Movement.Pause.performed += ctx => Pause();
            _panelPause.SetActive(false);
            _timePlayed = 0f;
        }

        private void Update()
        {
            CaptureInput();
        }

        private void FixedUpdate() 
        {
            if(_gamePaused) { return; }
            MoveCharacter();
            Look();

            _timePlayed += Time.deltaTime;
            
        }

        private void CaptureInput()
        {
            _moveInput = _inAcSaveLoadSystem.Movement.Move.ReadValue<Vector2>();
            _lookInput = _inAcSaveLoadSystem.Movement.Look.ReadValue<Vector2>();
        }

        private void MoveCharacter()
        {
            Vector3 vertMove = transform.forward * _moveInput.y;
            Vector3 horiMove = transform.right * _moveInput.x;
            _moveData = horiMove + vertMove;
            _moveData.Normalize();
            _moveData = _moveData * 5f;
            _characterController.Move(_moveData * Time.fixedDeltaTime);
        }

        private void Look()
        {
            Vector2 mouseInput = new Vector2(_lookInput.x * _lookSensitivityX, _lookInput.y * _lookSensitivityY);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

            _xRotation -= mouseInput.y;
            _xRotation = _xRotation = Mathf.Clamp(_xRotation, -65f, 65f);
            _camTrans.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        }

        private void Pause()
        {
            _gamePaused =! _gamePaused;
            if(_gamePaused)
            {
                _panelPause.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                _canSaved = true;
            } else {
                _panelPause.SetActive(false);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                _canSaved = false;
            }
        }

        public bool GetSave()
        {
            return _canSaved;
        }

        public void YouSave()
        {
            _canSaved = false;
        }

        public float GetTimePlayed()
        {
            return _timePlayed;
        }

        public void ResetTimePlayed()
        {
            _timePlayed = 0f;
        }

        public int GetCoins()
        {
            return _coins;
        }

        public void ResetCoins()
        {
            _coins = 0;
        }

        public void CoinPicked()
        {
            _coins++;
        }

    }

}