using UnityEngine;
using UnityEngine.UI;

public class SlideBook : MonoBehaviour
{
    public RectTransform bookPanel;
    public float slideDuration = 0.5f; // Duration of the sliding animation
    public Vector2 targetPosition; // Target position for the book panel (off-screen to the left)
    public GameObject slideToRightButton;
    public GameObject slideToLeftButton;
    private Vector2 originalPosition; // Original position of the book panel

    private void Start()
    {
        // Store the original position of the book panel
        originalPosition = bookPanel.anchoredPosition;
    }

    public void SlideBookToLeft()
    {
        if (slideToLeftButton.activeSelf) // Check if slide to left button is active
        {
            // Calculate the target position for sliding the book panel to the left (back to its original position)
            Vector3 targetPosition = new Vector3(originalPosition.x, bookPanel.transform.localPosition.y, bookPanel.transform.localPosition.z);

            // Move the book panel to the target position over a certain duration
            LeanTween.moveLocal(bookPanel.gameObject, targetPosition, slideDuration)
                .setEaseInOutQuad()
                .setOnComplete(() => {
                    // Enable the "slide to right" button
                    slideToRightButton.SetActive(true);
                });

            // Disable the "slide to left" button
            slideToLeftButton.SetActive(false);
        }
    }

    public void SlideBookToRight()
    {
        if (slideToRightButton.activeSelf) // Check if slide to right button is active
        {
            // Move the book panel to the target position over a certain duration
            LeanTween.move(bookPanel, targetPosition, slideDuration)
                .setEaseInOutQuad()
                .setOnComplete(() => {
                    // Enable the "slide to left" button
                    slideToLeftButton.SetActive(true);
                });

            // Disable the "slide to right" button
            slideToRightButton.SetActive(false);
        }
    }

    public void ResetBookPosition()
    {
        // Animate the book panel's position back to its original position
        LeanTween.move(bookPanel, originalPosition, slideDuration)
            .setEaseInOutQuad()
            .setOnComplete(() => {
                // Enable both slide buttons
                slideToLeftButton.SetActive(true);
                slideToRightButton.SetActive(true);
            });
    }
}
