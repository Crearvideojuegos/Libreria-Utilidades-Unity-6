using UnityEngine;

namespace SpaceFirstPersonScene
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Camera theCam;

        void LateUpdate()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }

}
