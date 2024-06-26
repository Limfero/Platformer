using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Attacker), typeof(Movement), typeof(Jumper))]
[RequireComponent(typeof(Vampirism))]
public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const string Fire1 = nameof(Fire1);

    private Attacker _attacker;
    private Movement _movement;
    private Jumper _jumper;
    private Vampirism _vampirism;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _movement = GetComponent<Movement>();
        _jumper = GetComponent<Jumper>();
        _vampirism = GetComponent<Vampirism>();
    }

    private void Update()
    {
        _movement.ChangeDirection(Input.GetAxis(Horizontal));
        _jumper.Jump(Input.GetAxis(Jump));

        if (Input.GetAxis(Fire1) > 0)
            _attacker.Attack();

        if(Input.GetKey(KeyCode.E))
            _vampirism.Activate();
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(1);
    }
}
