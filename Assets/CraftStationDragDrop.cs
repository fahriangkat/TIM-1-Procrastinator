using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftStationDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;

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
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Atur posisi item sesuai dengan posisi pointer
        rectTransform.anchoredPosition += eventData.delta / (GetComponentInParent<Canvas>().scaleFactor * Time.timeScale);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Kembalikan parent ke parent aslinya
        transform.SetParent(originalParent);

        // Aktifkan kembali interaksi dengan item setelah drag selesai
        canvasGroup.blocksRaycasts = true;

        // Cek apakah item di-drop di atas area inventory
        if (IsOverInventory(eventData))
        {
            MoveItemToInventory();
        }
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

    private void MoveItemToInventory()
    {
        // Dapatkan referensi ke item ScriptableObject
        Image image = GetComponent<Image>();
        Sprite iconSprite = image.sprite;
        Items itemData = GetItemDataFromIcon(iconSprite);

        // Pindahkan item dari craft station ke inventory jika item data tidak null
        if (itemData != null)
        {
            InventoryManager.Instance.AddItem(itemData);
            CraftStation.Instance.RemoveItem(itemData);
        }
        else
        {
            Debug.LogWarning("Item data not found!");
        }
    }

    private Items GetItemDataFromIcon(Sprite icon)
    {
        // Lakukan pencarian data item berdasarkan sprite icon pada craft station
        foreach (Items item in CraftStation.Instance.Items)
        {
            if (item.icon == icon)
            {
                return item;
            }
        }

        return null; // Return null jika tidak ditemukan
    }
}
