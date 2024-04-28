using UnityEngine;

[RequireComponent (typeof(Attacker))]
public class EnemyMovement : MonoBehaviour
{
    private const string Ground = nameof(Ground);

    [SerializeField] private Transform[] _checkPoints;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private VisibilityArea _visibilityArea;

    private int _currentPoint = 0;
    private int _rotationAngle = 180;
    private float _rayDistance = 1.5f;
    private bool _playerInZone = false;
    private float _inaccuracy = 0.25f;

    private PlayerController _player;
    private Attacker _attacker;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
    }

    private void Start()
    {
        foreach (var checkPoint in _checkPoints)
            checkPoint.position = new Vector3(checkPoint.position.x, transform.position.y, checkPoint.position.z);
    }

    private void OnEnable()
    {
        _visibilityArea.PlayerEntered += ChangeTargetToPlayer;
        _visibilityArea.PlayerOut += ChangeTargetToPoints;
    }

    private void OnDisable()
    {
        _visibilityArea.PlayerEntered -= ChangeTargetToPlayer;
        _visibilityArea.PlayerOut -= ChangeTargetToPoints;
    }

    private void Update()
    {
        if (_playerInZone)
            MoveToPlayer();
        else
            MoveToPoints();
    }

    private void MoveToPoints()
    {
        SetRotation(_checkPoints[_currentPoint]);

        if (Vector2.Distance(transform.position, _checkPoints[_currentPoint].position) < _inaccuracy)
        {
            _currentPoint = ++_currentPoint % _checkPoints.Length;
            SetRotation(_checkPoints[_currentPoint]);
        }

        transform.position = Vector2.MoveTowards(transform.position, _checkPoints[_currentPoint].position, _moveSpeed * Time.deltaTime);
    }

    private void MoveToPlayer()
    {
        GroundCheck();
        SetRotation(_player.transform);

        if (Vector2.Distance(transform.position, _player.transform.position) <= _attacker.Radius + _inaccuracy)
        {
            _attacker.Attack();
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

    private void ChangeTargetToPlayer(PlayerController player)
    {
        _playerInZone = true;
        _player = player;
    }

    private void ChangeTargetToPoints()
    {
        _playerInZone = false;
        _player = null;
    }

    private void SetRotation(Transform target)
    {
        if (target.position.x <= transform.position.x)
            transform.localRotation = Quaternion.Euler(0, _rotationAngle, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, Vector2.down, _rayDistance, LayerMask.GetMask(Ground));

        if (hit.collider == null)
            _playerInZone = false;
    }
}