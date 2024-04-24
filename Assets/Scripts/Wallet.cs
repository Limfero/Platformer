using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<float> Changed;

    private int _coinCount = 0;

    private void OnEnable()
    {
        Coin.Taken += Take;
    }

    private void OnDisable()
    {
        Coin.Taken -= Take;
    }

    private void Take() => Changed?.Invoke(++_coinCount);
}
