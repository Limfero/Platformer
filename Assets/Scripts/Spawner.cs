using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private float _delay;

    private void Start()
    {
        _coin.gameObject.SetActive(false);
        Spawn();
    }

    private void OnEnable()
    {
        _coin.Taken += Spawn;
    }

    private void OnDisable()
    {
        _coin.Taken -= Spawn;
    }

    private void Spawn() => StartCoroutine(Countdown());

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(_delay);

        _coin.gameObject.SetActive(true);
    }
}
