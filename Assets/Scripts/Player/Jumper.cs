using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(PlayerController))]
public class Jumper : MonoBehaviour
{
    private const string Ground = nameof(Ground);
    private const string IsJumping = nameof(IsJumping);

    [SerializeField] private float _force;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private PlayerController _playerController;
    private bool _isGrounded;
    private float _direction;

    private float _rayDistance = 1.5f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _playerController.ChangedJump += Jump;
    }

    private void OnDisable()
    {
        _playerController.ChangedJump -= Jump;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, LayerMask.GetMask(Ground));
        _isGrounded = hit.collider != null;
    }

    private void FixedUpdate()
    {
        if (_isGrounded && _direction > 0)
        {
            _animator.SetBool(IsJumping, true);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, _force), ForceMode2D.Impulse);
        }
    }

    private void Jump(float direction)
    {
        _direction = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetBool(IsJumping, false);
    }
}
