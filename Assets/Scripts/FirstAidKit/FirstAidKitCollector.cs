using UnityEngine;

public class FirstAidKitCollector : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_health.IsFullHealth == false && collision.TryGetComponent(out FirstAidKit firstAidKit))
        {
            _health.TakeHeal(firstAidKit.CountRecoveryHealth);
            Destroy(collision.gameObject);
        }
    }
}
