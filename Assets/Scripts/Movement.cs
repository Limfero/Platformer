using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Movement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Speed = nameof(Speed);

    [SerializeField] private float _moveSpeed;

    private int _rotationAngle = 180;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _direction;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction = Input.GetAxisRaw(Horizontal);
        _animator.SetFloat(Speed, Mathf.Abs(_direction * _moveSpeed));
        Rotate();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction * _moveSpeed, _rigidbody.velocity.y);
    }

    private void Rotate()
    {
        if (_direction > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (_direction < 0)
            transform.localRotation = Quaternion.Euler(0, _rotationAngle, 0);
    }
}
