using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Jumper : MonoBehaviour
{
    private const string Ground = nameof(Ground);
    private const string IsJumping = nameof(IsJumping);

    [SerializeField] private float _force;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private float _rayDistance = 1.5f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, LayerMask.GetMask(Ground));
        _isGrounded = hit.collider != null;

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _animator.SetBool(IsJumping, true);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, _force), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_isGrounded)
            _animator.SetBool(IsJumping, false);
    }
}
