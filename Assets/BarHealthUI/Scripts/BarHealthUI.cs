using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceBarHealthUI 
{

    public class BarHealthUI : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private TMP_Text healthText;
        private float valueMax;
        private float valueActual;

        private void Start()
        {
            valueMax = 10f;
            valueActual = 5f;
            RefreshUI();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A)) {
                if(valueActual > 0)
                {
                    valueActual -= 1f;
                    RefreshUI();
                }
            }

            if(Input.GetKeyDown(KeyCode.D)) {
                if(valueActual < valueMax)
                {
                    valueActual += 1f;
                    RefreshUI();
                }
            }
        }

        private void RefreshUI()
        {
            healthBar.fillAmount = valueActual / valueMax;
            healthText.text = $"{valueActual}/{valueMax}";
        }

    }

}
