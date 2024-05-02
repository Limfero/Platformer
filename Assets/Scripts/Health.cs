using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    public event Action Died;
    public event Action<float> Changed;

    public float MaxHealth => _maxHealth;

    public bool IsFullHealth => _currentHealth == _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeHeal(float heal)
    {
        if (heal < 0)
            return;

        _currentHealth = Math.Clamp(_currentHealth + heal, 0, _maxHealth);
        Changed?.Invoke(_currentHealth);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        _currentHealth = Math.Clamp(_currentHealth - damage, 0, _maxHealth);
        Changed?.Invoke(_currentHealth);

        if (_currentHealth == 0)
            Died?.Invoke();
    }
}
