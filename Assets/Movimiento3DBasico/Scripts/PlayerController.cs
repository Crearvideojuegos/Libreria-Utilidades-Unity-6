using UnityEngine;

namespace SpaceMovimiento3DBasico
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        private InAcPlayerControllerM3DBasico _inAcPlayerController;
        private Vector2 _moveInput;
        private CharacterController _characterController;
        private Animator animator;
        private Vector3 _moveData;
        private float _velocityY;
        private float _gravity = -9.81f;
        private bool _isMoving;
        [SerializeField] private Transform _targetCamera;
        [SerializeField] private GameObject _playerModel;

        //Animator
        private int _IdIsMovement;


        private void Awake()
        {
            if (Instance == null)
            { 
                Instance = this;
            } 
            _inAcPlayerController = new InAcPlayerControllerM3DBasico();
            _characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();

            _IdIsMovement = Animator.StringToHash("IsMovement");
        }

        private void Start()
        {
            _inAcPlayerController.Enable();
        }

        private void Update()
        {
            ReadMovementInput();
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            IsMoving();
        }

        private void MoveCharacter()
        {
            Vector3 forward = Vector3.forward;
            Vector3 right = Vector3.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            Vector3 direction = forward * _moveInput.y + right * _moveInput.x;
            _moveData = direction.normalized;

            _velocityY += _gravity * Time.deltaTime;
            _moveData.y = _velocityY;

            _characterController.Move(_moveData * Time.fixedDeltaTime * 7);

            if (_isMoving)
            {
                Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
                if (lookDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, targetRotation, Time.deltaTime * 10f);
                }
            }

            animator.SetBool(_IdIsMovement, _isMoving);
        }


        private void ReadMovementInput()
        {
            _moveInput = _inAcPlayerController.CharacterController.Move.ReadValue<Vector2>();
        }

        public void IsMoving()
        {
            _isMoving = _moveInput != Vector2.zero ? true : false;
        }

        private void OnDisable()
        {
            _inAcPlayerController.Disable();
        }

    }

}

