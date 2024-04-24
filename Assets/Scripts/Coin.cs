using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private const string Player = nameof(Player);

    public static event Action Taken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player))
        {
            Taken?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
