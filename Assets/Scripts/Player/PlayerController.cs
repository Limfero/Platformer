using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Attacker))]
public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const string Fire1 = nameof(Fire1);

    private Attacker _attacker;

    public event Action<float> ChangedDirection;
    public event Action<float> ChangedJump;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
    }

    private void Update()
    {
       ChangedDirection?.Invoke(Input.GetAxis(Horizontal));
       ChangedJump?.Invoke(Input.GetAxis(Jump));

        if (Input.GetAxis(Fire1) > 0)
            _attacker.Attack();
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}
