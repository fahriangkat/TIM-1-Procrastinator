using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private GameObject dragObject; // Object to be dragged

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // Create a duplicate of the item to be dragged
        dragObject = Instantiate(gameObject, transform.parent);
        dragObject.transform.position = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        // Update the position of the dragObject
        if (dragObject != null)
        {
            dragObject.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (dragObject != null)
        {
            // Check if the item is being dropped over a craft station
            GameObject craftStation = eventData.pointerCurrentRaycast.gameObject;
            if (craftStation != null && craftStation.CompareTag("CraftStation"))
            {
                // Perform the drop action
                CraftStation craftStationScript = craftStation.GetComponent<CraftStation>();
                if (craftStationScript != null)
                {
                    // Retrieve the item from the dragged GameObject
                    Items item = dragObject.GetComponent<Items>();

                    // Add the item to the craft station
                    if (item != null)
                    {
                        craftStationScript.AddItem(item);
                    }
                }
            }

            // Destroy the duplicate dragObject
            Destroy(dragObject);
        }
    }
}
