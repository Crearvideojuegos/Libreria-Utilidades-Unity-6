using TMPro;
using UnityEngine;

namespace SpaceFPSCounter
{
    public class FPSCounter : MonoBehaviour
    {
        private float _deltaTime = 0.0f;
        [SerializeField] private TMP_Text _fpsText;

        private void Start() 
        {
            Application.targetFrameRate = 60;    
        }

        private void Update() 
        {
            CountFPS();
        }

        private void CountFPS()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            float _fps = 1.0f / _deltaTime;
            _fpsText.text = Mathf.Ceil(_fps).ToString() + " FPS";
        }

    }

}
