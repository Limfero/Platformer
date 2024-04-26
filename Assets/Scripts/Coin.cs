using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action Taken;

    public void Take() => Taken?.Invoke();
}
