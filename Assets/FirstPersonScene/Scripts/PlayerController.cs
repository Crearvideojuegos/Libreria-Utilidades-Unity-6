using UnityEngine;

namespace FirstPersonScene
{
    public class PlayerController : MonoBehaviour
    {
        private InAcPlayerControllerFirstPersonScene _inAcPlayerController; //Input System
        private CharacterController _characterController;
        private float _xRotation;
        [SerializeField] private Transform _camTrans;
        private Vector2 _moveInput; //WASD Movement
        private Vector3 _moveData;
        private Vector2 _lookInput; //Mouse Look Movement

        [SerializeField] private float _lookSensitivityX = 0.1f;
        [SerializeField] private float _lookSensitivityY = 0.1f;
        
        private void Awake() 
        {
            _inAcPlayerController = new InAcPlayerControllerFirstPersonScene();
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            _inAcPlayerController.Enable();
            _inAcPlayerController.Movement.CustomAction.performed += ctx => CallCustomAction();

        }

        private void Update()
        {
            CaptureInput();
        }

        private void FixedUpdate() 
        {
            MoveCharacter();
            Look();
        }

        private void CaptureInput()
        {
            _moveInput = _inAcPlayerController.Movement.Move.ReadValue<Vector2>();
            _lookInput = _inAcPlayerController.Movement.Look.ReadValue<Vector2>();
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

        private void CallCustomAction()
        {
            Debug.Log("Estas llamando a la CallCustomAction");
        }

    }

}