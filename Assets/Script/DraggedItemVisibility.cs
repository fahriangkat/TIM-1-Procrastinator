using UnityEngine;
using UnityEngine.EventSystems;

public class DraggedItemVisibility : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Ketika drag dimulai, sembunyikan item
        canvasGroup.alpha = 0f; // Set transparansi menjadi nol
        canvasGroup.blocksRaycasts = false; // Jangan blokir raycast saat didrag
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Tidak diperlukan tindakan khusus selama drag berlangsung
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Ketika drag selesai, tampilkan kembali item
        canvasGroup.alpha = 1f; // Set transparansi menjadi penuh
        canvasGroup.blocksRaycasts = true; // Kembalikan blokir raycast ke nilai awal
    }
}
