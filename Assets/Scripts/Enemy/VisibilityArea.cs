using System;
using UnityEngine;

public class VisibilityArea : MonoBehaviour
{
    public event Action<PlayerController> PlayerEntered;
    public event Action PlayerOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
            PlayerEntered?.Invoke(player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
            PlayerOut?.Invoke();
    }
}
