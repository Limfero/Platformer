using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _coinCount = 0;

    public event Action<float> Changed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            Changed?.Invoke(++_coinCount);

            coin.Take();
            collision.gameObject.SetActive(false);
        }
    }
}
