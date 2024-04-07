using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Tambahkan variabel instance

    // Metode lainnya

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Inisialisasi instance
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Metode lainnya
}
