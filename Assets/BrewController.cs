using UnityEngine;

public class BrewController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ketika ingredient masuk ke dalam Collider brew
        if (other.CompareTag("Ingredient"))
        {
            // Hilangkan ingredient
            Destroy(other.gameObject);
        }
    }
}