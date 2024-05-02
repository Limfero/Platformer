using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private void Update()
    {
        if(_target == false)
            Destroy(gameObject);
        else
            transform.position = _target.transform.localPosition;
    }
}
