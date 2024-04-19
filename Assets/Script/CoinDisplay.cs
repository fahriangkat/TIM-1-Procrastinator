using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI coinText; // TMP Text untuk menampilkan jumlah koin

    private void Start()
    {
        UpdateCoinDisplay(); // Panggil method ini saat awal
    }

    // Method untuk memperbarui tampilan jumlah koin
    public void UpdateCoinDisplay()
    {
        // Dapatkan jumlah koin dari ShopManager
        int coins = ShopManager.Instance.GetCoins();

        // Perbarui teks pada layar dengan jumlah koin yang dimiliki
        coinText.text = ": " + coins.ToString();
    }

    // Method untuk menambahkan jumlah koin
   public void AddCoins(int amount)
{
    ShopManager.Instance.AddCoins(amount);
    UpdateCoinDisplay(); // Memperbarui tampilan koin setelah menambahkan koin
}
}
