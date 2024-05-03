using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health), typeof(Collider2D))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private float _actionTime = 6f;
    [SerializeField] private float _pullRecharge = 1f;
    [SerializeField] private float _abilityRecharge = 12f;
    [SerializeField] private float _radius = 7f;
    [SerializeField] private float _healthIntake = 1f;

    private Health _health;
    private Collider2D _collider;
    private Color _defaultCollor;

    private bool _canActivate = true;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _health = GetComponent<Health>();

        _defaultCollor = _icon.color;
    }

    public void Activate()
    {
        if (_canActivate)
        {
            _icon.color = Color.red;
            _canActivate = false;
            StartCoroutine(Countdown());
        }
    }

    private IEnumerator Countdown()
    {
        var wait = new WaitForSeconds(_pullRecharge);
        int numberApplications = (int)(_actionTime / _pullRecharge);

        while (numberApplications-- > 0)
        {
            PullHealth();

            yield return wait;
        }

        StartCoroutine(Recharge());
    }

    private IEnumerator Recharge()
    {
        _icon.color = Color.gray;

        yield return new WaitForSeconds(_abilityRecharge);

        _canActivate = true;
        _icon.color = _defaultCollor;
    }

    private void PullHealth()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (var hit in hits)
        {
            if (hit != _collider && hit.TryGetComponent(out DamageTaker target))
            {
                target.TakeDamage(transform.position, _healthIntake);
                _health.TakeHeal(_healthIntake);
            }
        }
    }
}
