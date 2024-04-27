using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider2D))]
public class Attacker : MonoBehaviour
{
    private const string AttackTrigger = nameof(AttackTrigger);

    [SerializeField] private float _damage;
    [SerializeField] private float _reload;
    [SerializeField] private float _radius;
    [SerializeField] private Transform _weaponPosition;

    public float Radius => _radius;

    private Animator _animator;
    private Collider2D _collider;

    private bool _canAttack = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    public void Attack()
    {
        if (_canAttack)
        {
            _canAttack = false;
            _animator.SetTrigger(AttackTrigger);
            
            StartCoroutine(Countdown());
        }
    }

    private void AttackToggle()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_weaponPosition.position, _radius);

        foreach (var hit in hits)
            if (hit != _collider && hit.TryGetComponent(out DamageTaker target))
                target.TakeDamage(transform.position, _damage);
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(_reload);

        _canAttack = true;
    }
}
