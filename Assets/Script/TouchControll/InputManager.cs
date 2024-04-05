using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public InputAction TouchInput { get; private set; }

    private void Awake()
    {
        // Membuat singleton untuk InputManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return; // Kembalikan nilai jika Instance sudah ada
        }

        // Menginisialisasi action
        TouchInput = new InputAction(binding: "<Touchscreen>/primaryTouch");

        // Menambahkan callback saat aksi terjadi
        TouchInput.started += ctx => OnTouchStarted(ctx);
        TouchInput.performed += ctx => OnTouchPerformed(ctx);
        TouchInput.canceled += ctx => OnTouchCanceled(ctx);
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        // Tangani sentuhan saat dimulai
        Debug.Log("Touch started.");
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        // Tangani sentuhan saat dilakukan
        Debug.Log("Touch performed.");
    }

    private void OnTouchCanceled(InputAction.CallbackContext context)
    {
        // Tangani sentuhan saat dibatalkan
        Debug.Log("Touch canceled.");
    }

    private void OnEnable()
    {
        // Mengaktifkan action saat objek diaktifkan
        TouchInput.Enable();
    }

    private void OnDisable()
    {
        // Menonaktifkan action saat objek dinonaktifkan
        TouchInput.Disable();
    }
}