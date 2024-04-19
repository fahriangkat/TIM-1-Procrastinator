using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform dragRectTransform;
    private Transform originalParent;
    private Transform canvasTransform;
    private Transform craftContentTransform;

    private void Start()
    {
        originalParent = transform.parent;
        canvasTransform = GameObject.Find("Canvas").transform;
        craftContentTransform = GameObject.Find("CraftContent").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragRectTransform = GetComponent<RectTransform>();
        originalParent = transform.parent;
        transform.SetParent(canvasTransform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvasTransform == null) return;
        dragRectTransform.anchoredPosition += eventData.delta / canvasTransform.localScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragRectTransform = null;

        // Check if item is over CraftStation
        if (IsPointerOverCraftStation())
        {
            // Set parent to CraftStation if item is dropped over it
            transform.SetParent(craftContentTransform);
        }
        else
        {
            // Set parent back to original parent if item is dropped outside CraftStation
            transform.SetParent(originalParent);
        }
    }

    public void OnDrop(PointerEventData eventData)
{
    // Panggil metode ListItem dari CraftStation ketika item di-drop di atasnya
    CraftStation.Instance.ListItem();
}

private bool IsPointerOverCraftStation()
{
    // Cek apakah pointer berada di atas CraftStation dengan membandingkan tag
    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    return hit.collider != null && hit.collider.CompareTag("CraftStation");
}

}
