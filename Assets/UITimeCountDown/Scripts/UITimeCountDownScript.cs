using TMPro;
using UnityEngine;

namespace NameUITimeCountDownScript
{
    public class UITimeCountDownScript : MonoBehaviour
    {
        public TextMeshProUGUI TimeText;

        private float _timeRemaining = 60f;

        private void Update() 
        {

            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            } else {
                Debug.Log("Finsih Time");
            }

            TimeText.text = "Time: " + _timeRemaining.ToString("f0");            
            TimeText.text = "Time: " + _timeRemaining.ToString("F2"); //2 Decimals with Points
            TimeText.text = "Time: " + _timeRemaining.ToString("F2", System.Globalization.CultureInfo.InvariantCulture); //2 Decimals

        }
    }

}
