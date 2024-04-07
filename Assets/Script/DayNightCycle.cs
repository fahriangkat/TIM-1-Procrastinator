using UnityEngine;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle Instance;

    public int currentDay = 1; // Hari saat ini
    public int dayDurationMinutes = 5; // Durasi setiap hari dalam menit
    public TextMeshProUGUI dayText; // Teks untuk menampilkan hari

    private float secondsPerDay; // Durasi setiap hari dalam detik

    private void Awake()
    {
        Instance = this;
        secondsPerDay = dayDurationMinutes * 60f; // Konversi durasi harian menjadi detik
    }

    private void Start()
    {
        UpdateDayText(); // Memperbarui teks hari saat permainan dimulai
    }

    private void Update()
    {
        // Hitung waktu berdasarkan detik
        float totalSeconds = Time.time;
        float cycle = totalSeconds % secondsPerDay; // Menghitung siklus waktu harian

        // Hitung hari berdasarkan siklus waktu harian
        int day = Mathf.FloorToInt(totalSeconds / secondsPerDay) + 1;

        // Perbarui hari jika berubah
        if (day != currentDay)
        {
            currentDay = day;
            UpdateDayText(); // Memperbarui teks hari
        }
    }

    private void UpdateDayText()
    {
        if (dayText != null)
        {
            dayText.text = "Day " + currentDay.ToString(); // Memperbarui teks hari
        }
    }
}
