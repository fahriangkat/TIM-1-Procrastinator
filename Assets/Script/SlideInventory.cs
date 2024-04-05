using UnityEngine;
using UnityEngine.UI;

public class SlideInventory : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public float slideDuration = 0.5f; // Duration of the sliding animation
    public Vector2 targetPosition; // Target position for the inventory panel (off-screen to the left)
    public GameObject slideToRightButton;
    public GameObject slideToLeftButton;
    private Vector2 originalPosition; // Original position of the inventory panel

    private void Start()
    {
        // Store the original position of the inventory panel
        originalPosition = inventoryPanel.anchoredPosition;
    }

    public void SlideInventoryToLeft()
{
    // Pastikan callback OnSlideComplete tidak menambahkan item kembali ke inventaris
    LeanTween.move(inventoryPanel, targetPosition, slideDuration)
        .setEaseInOutQuad()
        .setOnComplete(() => {
            // Enable the "slide to right" button
            ToggleSlideButtons(false, true);
            // Pastikan tidak ada logika yang menambah item kembali ke inventaris di sini
        });
}


    public void SlideInventoryToRight()
    {
        // Calculate the target position for sliding the inventory panel to the right
        Vector3 targetPosition = new Vector3(originalPosition.x, inventoryPanel.transform.localPosition.y, inventoryPanel.transform.localPosition.z);
        
        // Move the inventory panel to the target position over a certain duration
        LeanTween.moveLocal(inventoryPanel.gameObject, targetPosition, slideDuration);
        
        // Enable the "slide to left" button and disable the "slide to right" button
        ToggleSlideButtons(true, false);
    }

    private void OnSlideComplete()
    {
        // Optionally, you can perform any additional actions after the sliding animation completes
        Debug.Log("Inventory panel slid to the left!");
    }

    public void ResetInventoryPosition()
{
    // Animate the inventory panel's position back to its original position
    LeanTween.move(inventoryPanel, originalPosition, slideDuration)
        .setEaseInOutQuad()
        .setOnComplete(() => {
            // Pastikan tidak ada logika yang menambah item kembali ke inventaris di sini
        });
}


    public void ToggleSlideButtons(bool slideToLeftButtonVisible, bool slideToRightButtonVisible)
    {
        slideToLeftButton.SetActive(slideToLeftButtonVisible);
        slideToRightButton.SetActive(slideToRightButtonVisible);
    }
}
