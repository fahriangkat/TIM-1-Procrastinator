using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour, IPointerClickHandler
{
    private bool isTouched; // Menyimpan status sentuhan
    private float lastTapTime; // Waktu terakhir sentuhan terdeteksi

    private void OnEnable()
    {
        // Langganan event untuk aksi 'primaryTouch'
        InputManager.Instance.TouchInput.started += OnTouchStarted;
        InputManager.Instance.TouchInput.performed += OnTouchPerformed;
        InputManager.Instance.TouchInput.canceled += OnTouchCanceled;
    }

    private void OnDisable()
    {
        // Batalkan langganan event saat objek dinonaktifkan
        InputManager.Instance.TouchInput.started -= OnTouchStarted;
        InputManager.Instance.TouchInput.performed -= OnTouchPerformed;
        InputManager.Instance.TouchInput.canceled -= OnTouchCanceled;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1) // Single tap detected
        {
            Debug.Log("Single tap detected.");
            HandleSingleTap();
        }
        else if (eventData.clickCount == 2) // Double tap detected
        {
            Debug.Log("Double tap detected.");
            HandleDoubleTap();
        }
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        isTouched = true; // Set status sentuhan menjadi true saat sentuhan dimulai
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        // Tidak ada yang perlu dilakukan pada saat sentuhan sedang dilakukan
    }

    private void OnTouchCanceled(InputAction.CallbackContext context)
    {
        isTouched = false; // Set status sentuhan menjadi false saat sentuhan dibatalkan
    }

    private void HandleSingleTap()
    {
        if (isTouched)
        {
            // Tambahkan logika untuk menangani single tap pada item di sini
            Debug.Log("Handling single tap on item.");
        }
    }

    private void HandleDoubleTap()
    {
        if (isTouched)
        {
            float currentTime = Time.time;
            float timeSinceLastTap = currentTime - lastTapTime;
            lastTapTime = currentTime;

            if (timeSinceLastTap < 0.5f) // Jika double tap terjadi dalam rentang waktu 0.5 detik
            {
                // Tambahkan logika untuk menangani double tap pada item di sini
                Debug.Log("Handling double tap on item.");
                MoveItemToCraftStation();
            }
        }
    }

    private void MoveItemToCraftStation()
    {
        // Tambahkan logika untuk memindahkan item dari inventory ke craft station di sini
        Debug.Log("Moving item to craft station.");
    }
}
