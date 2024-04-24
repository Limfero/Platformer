using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.Changed += DisplayCoin;
    }

    private void OnDisable()
    {
        _wallet.Changed -= DisplayCoin;
    }

    private void DisplayCoin(float coins) => _coinText.text = coins.ToString();
}
