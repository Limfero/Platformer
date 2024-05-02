using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Movement : MonoBehaviour
{
    private const string Speed = nameof(Speed);

    [SerializeField] private float _moveSpeed;

    private int _rotationAngle = 180;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private float _direction;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction * _moveSpeed, _rigidbody.velocity.y);
    }

    public void ChangeDirection(float direction)
    {
        _direction = direction;
        _animator.SetFloat(Speed, Mathf.Abs(_direction * _moveSpeed));

        if (_direction != 0)
            Rotate();
    }

    private void Rotate()
    {
        if (_direction > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            transform.localRotation = Quaternion.Euler(0, _rotationAngle, 0);
    }
}
