using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _checkPoints;
    [SerializeField] private float _moveSpeed;

    private int _currentPoint = 0;
    private int _rotationAngle = 180;

    private void Start()
    {
        foreach (var checkPoint in _checkPoints)
            checkPoint.position = new Vector3(checkPoint.position.x, transform.position.y, checkPoint.position.z);

        SetRotation();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _checkPoints[_currentPoint].position) < 0.2)
        {
            _currentPoint = ++_currentPoint % _checkPoints.Length;
            SetRotation();
        }

        transform.position = Vector2.MoveTowards(transform.position, _checkPoints[_currentPoint].position, _moveSpeed * Time.deltaTime);
    }

    private void SetRotation()
    {
        if (_checkPoints[_currentPoint].position.x < transform.position.x)
            transform.rotation = Quaternion.Euler(0, _rotationAngle, 0);      
        else if(_checkPoints[_currentPoint].position.x > transform.position.x)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
