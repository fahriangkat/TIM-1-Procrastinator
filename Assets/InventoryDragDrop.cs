using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;

    private Vector2 offset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Ketika drag dimulai, set parent ke canvas agar tetap terlihat di atas UI lainnya
        transform.SetParent(transform.root);

        // Nonaktifkan interaksi dengan item selama sedang di-drag
        canvasGroup.blocksRaycasts = false;

        // Hitung offset dari titik klik ke pivot item
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Atur posisi item sesuai dengan posisi pointer
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            rectTransform.localPosition = localPointerPosition - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Kembalikan parent ke parent aslinya
        transform.SetParent(originalParent);

        // Aktifkan kembali interaksi dengan item setelah drag selesai
        canvasGroup.blocksRaycasts = true;

        // Cek apakah item di-drop di atas craft station atau di luar area inventory
        if (IsOverCraftStation(eventData) || !IsOverInventory(eventData))
        {
            MoveItemToCraftStation();
        }
    }

    private bool IsOverCraftStation(PointerEventData eventData)
    {
        // Buat raycast dari posisi pointer
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        
        // Periksa setiap hasil raycast
        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject.CompareTag("CraftStation"))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsOverInventory(PointerEventData eventData)
    {
        // Buat raycast dari posisi pointer
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        
        // Periksa setiap hasil raycast
        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject.CompareTag("Inventory"))
            {
                return true;
            }
        }

        return false;
    }

    private void MoveItemToCraftStation()
    {
        // Dapatkan referensi ke item ScriptableObject
        Image image = GetComponent<Image>();
        Sprite iconSprite = image.sprite;
        Items itemData = GetItemDataFromIcon(iconSprite);

        // Pindahkan item dari inventory ke craft station jika item data tidak null
        if (itemData != null)
        {
            CraftStation.Instance.AddItem(itemData);
            InventoryManager.Instance.Remove(itemData);
        }
        else
        {
            Debug.LogWarning("Item data not found!");
        }
    }

    private Items GetItemDataFromIcon(Sprite icon)
    {
        // Lakukan pencarian data item berdasarkan sprite icon
        foreach (Items item in InventoryManager.Instance.Items)
        {
            if (item.icon == icon)
            {
                return item;
            }
        }

        return null; // Return null jika tidak ditemukan
    }
}
