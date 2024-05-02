using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;

    private float _smoothDecreaseDuration = 0.5f;

    private Slider _slider;
    private Coroutine _coroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = _health.MaxHealth;
        _slider.value = _health.MaxHealth;
    }

    private void OnEnable()
    {
        _health.Changed += View;
    }

    private void OnDisable()
    {
        _health.Changed -= View;
    }

    private void View(float health)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Countdown(health));
    }

    private IEnumerator Countdown(float health)
    {
        float elasedTime = 0f;
        float currentValue = _slider.value;

        while (elasedTime < _smoothDecreaseDuration) 
        {
            elasedTime += Time.deltaTime;
            float normalizedPosition = elasedTime / _smoothDecreaseDuration;

            _slider.value = Mathf.Lerp(currentValue, health, normalizedPosition);

            yield return null;
        }
    }
}
