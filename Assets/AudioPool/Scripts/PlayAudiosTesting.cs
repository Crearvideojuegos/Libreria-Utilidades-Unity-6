using UnityEngine;

namespace SpaceAudioPool
{
    public class PlayAudiosTesting : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _testingSounds;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                AudioEffectsManager.Instance.PlaySound(_testingSounds[0], Vector3.zero);
            }

            if(Input.GetKeyDown(KeyCode.W))
            {
                AudioEffectsManager.Instance.PlaySound(_testingSounds[1], Vector3.zero);
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                AudioEffectsManager.Instance.PlaySound(_testingSounds[2], Vector3.zero);
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                AudioEffectsManager.Instance.PlaySound(_testingSounds[3], Vector3.zero);
            }

            if(Input.GetKeyDown(KeyCode.T))
            {
                AudioEffectsManager.Instance.PlaySound(_testingSounds[4], Vector3.zero);
            }
        }
    }
}