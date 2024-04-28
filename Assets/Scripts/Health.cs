using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    public event Action Died;

    public bool IsFullHealth => _currentHealth == _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeHeal(float heal)
    {
        _currentHealth = Math.Clamp(_currentHealth + heal, 0, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Math.Clamp(_currentHealth - damage, 0, _maxHealth);

        if (_currentHealth == 0)
            Died?.Invoke();
    }
}
