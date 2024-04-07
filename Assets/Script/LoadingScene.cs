using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingScreen; // Referensi ke GameObject yang memiliki Animator
    public string animationName = "LoadingAnimation"; // Nama animasi loading

    // Fungsi untuk memulai loading scene berdasarkan indeks scene
    public void LoadSceneByIndex(int sceneIndex)
    {
        StartCoroutine(BeginLoad(sceneIndex));
    }

    private IEnumerator BeginLoad(int sceneIndex)
    {
        loadingScreen.SetActive(true); // Aktifkan panel loading
        
        // Dapatkan komponen Animator dan mainkan animasi
        Animator animator = loadingScreen.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play(animationName);
        }
        else
        {
            Debug.LogError("Animator component not found on the loading screen object.");
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true; // Izinkan scene untuk diaktifkan

                // Tunggu sedikit sebelum menonaktifkan panel loading
                // Ini opsional dan bisa dihilangkan jika tidak diperlukan
                yield return new WaitForSeconds(1);

                loadingScreen.SetActive(false); // Nonaktifkan panel loading
            }

            yield return null;
        }
    }
}
