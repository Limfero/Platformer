using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private float _countRecoveryHealth;

    public float CountRecoveryHealth => _countRecoveryHealth;
}
