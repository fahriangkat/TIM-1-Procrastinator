using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    public Dropdown dropdown;

    void Start()
    {
        // Tambahkan listener ke event OnValueChanged dari dropdown
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }

    // Metode yang akan dipanggil ketika pengguna memilih opsi dropdown
    void DropdownValueChanged(Dropdown dropdown)
    {
        int selectedOptionIndex = dropdown.value;
        string selectedOptionText = dropdown.options[selectedOptionIndex].text;
        
        // Lakukan sesuatu berdasarkan opsi yang dipilih
        Debug.Log("Selected option: " + selectedOptionText);
    }
}
