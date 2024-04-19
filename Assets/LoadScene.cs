using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int nextSceneIndex = 2; // Indeks scene berikutnya (misalnya, "SampleScene" memiliki indeks 2)
    public float loadingTime = 15f;

    void Start()
    {
        // Mulai proses loading secara otomatis setelah waktu tertentu
        Invoke("LoadNextScene", loadingTime);
    }

    void LoadNextScene()
    {
        // Memuat scene berikutnya berdasarkan indeks
        SceneManager.LoadScene(nextSceneIndex);
    }
}
