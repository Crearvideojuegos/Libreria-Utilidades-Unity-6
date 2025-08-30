using UnityEngine;

namespace SpaceMovimiento3DBasico
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private Transform _targetCamera;

        private void LateUpdate()
        {
            transform.position = _targetCamera.position;
            transform.rotation = _targetCamera.rotation;
        }

    }
}