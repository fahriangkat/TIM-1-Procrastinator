using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class DayNightController : MonoBehaviour
{
    public float dayDurationMinutes = 1f; // Durasi satu hari dalam menit (1 menit)
    public TMP_Text dayText; // Referensi ke TMP Text untuk menampilkan jumlah hari

    private DateTime gameStartTime; // Waktu mulai permainan
    private int dayCount = 1; // Hitungan hari

    private void Start()
    {
        // Cek apakah permainan pernah dimainkan sebelumnya
        if (PlayerPrefs.HasKey("GameStartTime"))
        {
            // Ambil waktu mulai permainan
            long ticks = Convert.ToInt64(PlayerPrefs.GetString("GameStartTime"));
            gameStartTime = new DateTime(ticks);
        }
        else
        {
            // Jika belum pernah dimainkan sebelumnya, set waktu mulai permainan menjadi waktu sekarang
            gameStartTime = DateTime.Now;
            PlayerPrefs.SetString("GameStartTime", gameStartTime.Ticks.ToString()); // Menyimpan waktu pertama kali dimulai
        }

        // Pastikan referensi dayText sudah diisi di Inspector
        if (dayText == null)
        {
            Debug.LogError("Day Text reference is not set!");
            return;
        }

        StartCoroutine(UpdateDayNightCycle());
    }

 private IEnumerator UpdateDayNightCycle()
{
    while (true)
    {
        // Hitung waktu sejak game dimulai
        TimeSpan timeSinceStart = DateTime.Now - gameStartTime;

        // Hitung jumlah hari yang telah berlalu
        int daysPassed = (int)(timeSinceStart.TotalMinutes / dayDurationMinutes);

        // Jika lebih dari 30 hari telah berlalu, reset hitungan hari
        if (daysPassed >= 30)
        {
            dayCount = 1;
            PlayerPrefs.SetString("GameStartTime", DateTime.Now.Ticks.ToString()); // Reset waktu mulai permainan
            Debug.Log("Game reset. Day 1.");
        }
        else
        {
            // Update hitungan hari
            dayCount = daysPassed + 1; // Dimulai dari hari ke-1
        }

        // Tampilkan day yang sedang berjalan pada konsol
        Debug.Log("Hari " + dayCount);

        // Perbarui teks dayText dengan dayCount terbaru
        UpdateDayText();

        // Tambahkan coin setiap hari
        if (daysPassed % 7 == 0)
        {
            ShopManager.Instance.AddDailyCoins(5); // Menambahkan 5 coin setiap hari ke-7
        }
        else
        {
            ShopManager.Instance.AddDailyCoins(1); // Menambahkan 1 coin setiap hari lainnya
        }

        yield return new WaitForSeconds(dayDurationMinutes * 60); // Tunggu satu menit (durasi siklus daynight)
    }
}


    // Method untuk memperbarui teks dayText dengan dayCount terbaru
    private void UpdateDayText()
    {
        dayText.text = "Day " + dayCount.ToString();
    }

    // Mendapatkan jumlah hari saat ini dalam permainan
    public int GetCurrentDay()
    {
        return dayCount;
    }
}
