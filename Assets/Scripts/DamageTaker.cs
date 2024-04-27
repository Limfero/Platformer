using UnityEngine;

[RequireComponent (typeof(Animator), typeof(Rigidbody2D), typeof(Collider2D))]
public class DamageTaker : MonoBehaviour
{
    private const string TakedDamageTrigger = nameof(TakedDamageTrigger);
    private const string DeathTrigger = nameof(DeathTrigger);

    [SerializeField] private Health _health;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private float _force = 3f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _health.Death += Death;
    }

    private void OnDisable()
    {
        _health.Death -= Death;
    }

    public void TakeDamage(Vector2 positionAttacker, float damage)
    {
        _health.TakeDamage(damage);
        positionAttacker = positionAttacker.x - transform.position.x < 0 ? Vector2.right : Vector2.left;

        _rigidbody.AddForce(new Vector2(positionAttacker.x * _force, 0), ForceMode2D.Impulse);
        _animator.SetTrigger(TakedDamageTrigger);
    }

    private void Death()
    {
        _animator.SetTrigger(DeathTrigger);

        _rigidbody.isKinematic = true;
        _collider.enabled = false;
    }

    private void DeathToggle()
    {
        Destroy(gameObject);
    }
}
