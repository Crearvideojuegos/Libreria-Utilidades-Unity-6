using UnityEngine;

namespace SpaceStayZone
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance; //Singleton
        private InAcStayZone _inAcStayZone; //Input System
        private CharacterController _characterController;
        private float _xRotation;
        [SerializeField] private Transform _camTrans;
        private Vector2 _moveInput; //WASD Movement
        private Vector3 _moveData;
        private Vector2 _lookInput; //Mouse Look Movement
        [SerializeField] private float _lookSensitivityX = 0.1f;
        [SerializeField] private float _lookSensitivityY = 0.1f;
        public bool IsInZone = true;
        public float TimeRemaining = 5f;

        private void Awake() 
        {
            Instance = this;
            _inAcStayZone = new InAcStayZone();
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            _inAcStayZone.Enable();
        }

        private void Update()
        {
            CaptureInput();
            if(!IsInZone)
            {
                if (TimeRemaining > 0)
                {
                    TimeRemaining -= Time.deltaTime;
                    Debug.Log("TimeRemaining: " + TimeRemaining);
                }
                else
                {
                    Debug.Log("You are dead");
                }
            }
        }

        private void FixedUpdate() 
        {
            MoveCharacter();
            Look();            
        }

        private void CaptureInput()
        {
            _moveInput = _inAcStayZone.Movement.Move.ReadValue<Vector2>();
            _lookInput = _inAcStayZone.Movement.Look.ReadValue<Vector2>();
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



    }

}