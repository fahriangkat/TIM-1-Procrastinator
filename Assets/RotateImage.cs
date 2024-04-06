using UnityEngine;

public class RotateImage : MonoBehaviour
{
    public Transform imageTransform; // Referensi ke transform gambar yang akan dirotasi
    public DayNightController dayNightController; // Referensi ke DayNightController untuk mendapatkan waktu

    private float rotationSpeed; // Kecepatan rotasi (dalam derajat per detik)

    private void Start()
    {
        // Hitung kecepatan rotasi berdasarkan durasi satu hari dalam daynight cycle
        float rotationDegreesPerDay = -360f; // 360 derajat rotasi dalam satu hari
        float dayDurationMinutes = dayNightController.dayDurationMinutes; // Durasi satu hari dalam menit
        rotationSpeed = rotationDegreesPerDay / (dayDurationMinutes * 60f); // Kecepatan rotasi (derajat/detik)
    }

    private void Update()
    {
        // Atur rotasi gambar
        imageTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
