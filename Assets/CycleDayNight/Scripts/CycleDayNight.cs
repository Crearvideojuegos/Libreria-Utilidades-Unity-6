using UnityEngine;

namespace SpaceCycleDayNight
{
    public class CycleDayNight : MonoBehaviour
    {
        [Range(0.0f, 1.0f)] private float _time;
        [SerializeField] private float _fulldayLength;
        [SerializeField] private float _startTime = 0.5f;
        [SerializeField] private float _timeRate;
        [SerializeField] private Vector3 _noon;

        [Header("Sun Setting")]
        [SerializeField] private Light _sun;
        [SerializeField] private Gradient _sunColor;
        [SerializeField] private AnimationCurve _sunIntensity;

        [Header("Moon Setting")]
        [SerializeField] private Light _moon;
        [SerializeField] private Gradient _moonColor;
        [SerializeField] private AnimationCurve _moonIntensity;

        [Header("Lighting Setting")]
        [SerializeField] private AnimationCurve _lightingIntensityMultiplier;
        [SerializeField] private AnimationCurve _reflectionsIntensityMultiplier;

        private void Start()
        {
            _timeRate = 1.0f / _fulldayLength;
            _time = _startTime;
        }

        private void Update()
        {
            _time += _timeRate * Time.deltaTime;

            if(_time >= 1.0f)
            {
                _time = 0.0f;
            }

            //Rotation
            _sun.transform.eulerAngles = (_time - 0.25f) * _noon * 4.0f;
            _moon.transform.eulerAngles = (_time - 0.75f) * _noon * 4.0f;

            //Intensity
            _sun.intensity = _sunIntensity.Evaluate(_time);
            _moon.intensity = _moonIntensity.Evaluate(_time);

            //Colors
            _sun.color = _sunColor.Evaluate(_time);
            _moon.color = _moonColor.Evaluate(_time);

            //Enable or Disable Sun and Moon
            if(_sun.intensity == 0 && _sun.gameObject.activeInHierarchy)
            {
                _sun.gameObject.SetActive(false);
            }
            else if(_sun.intensity > 0 && !_sun.gameObject.activeInHierarchy)
            {
                _sun.gameObject.SetActive(true);
            }

            if (_moon.intensity == 0 && _moon.gameObject.activeInHierarchy)
            {
                _moon.gameObject.SetActive(false);
            } 
            else if(_moon.intensity > 0 && !_moon.gameObject.activeInHierarchy)
            {
                _moon.gameObject.SetActive(true);
            }

            //Intensity Light and Reflections
            RenderSettings.ambientIntensity = _lightingIntensityMultiplier.Evaluate(_time);
            RenderSettings.reflectionIntensity = _reflectionsIntensityMultiplier.Evaluate(_time);
        }
    }
}
