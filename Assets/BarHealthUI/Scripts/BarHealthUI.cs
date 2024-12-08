using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceBarHealthUI 
{

    public class BarHealthUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TMP_Text _healthText;
        private float _valueMax;
        private float _valueActual;

        private void Start()
        {
            _valueMax = 10f;
            _valueActual = 5f;
            RefreshUI();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A)) {
                if(_valueActual > 0)
                {
                    _valueActual -= 1f;
                    RefreshUI();
                }
            }

            if(Input.GetKeyDown(KeyCode.D)) {
                if(_valueActual < _valueMax)
                {
                    _valueActual += 1f;
                    RefreshUI();
                }
            }
        }

        private void RefreshUI()
        {
            _healthBar.fillAmount = _valueActual / _valueMax;
            _healthText.text = $"{_valueActual}/{_valueMax}";
        }

    }

}
