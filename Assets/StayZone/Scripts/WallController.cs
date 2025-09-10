using UnityEngine;

namespace SpaceStayZone
{
    public class WallController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag("Player"))
            {
                if (PlayerController.Instance.IsInZone)
                {
                    PlayerController.Instance.IsInZone = false;
                    PlayerController.Instance.TimeRemaining = 5f;
                }
                else
                {
                    PlayerController.Instance.IsInZone = true;
                }
            }
        }
    }
}