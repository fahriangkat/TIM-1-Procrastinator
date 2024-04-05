using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropdownOption : MonoBehaviour
{
    // Dropdown untuk menambahkan opsi ke dalamnya
    public Dropdown dropdown;

    // Opsi yang akan ditambahkan ke dalam dropdown
    public List<string> options = new List<string>{"Option 1", "Option 2", "Option 3"};

    private void Start()
    {
        // Bersihkan opsi dropdown
        dropdown.ClearOptions();

        // Tambahkan opsi ke dropdown
        dropdown.AddOptions(options);
    }
}
