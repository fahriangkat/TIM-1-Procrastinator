using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        Debug.Log("Exiting game...");
        Application.Quit(); // Keluar dari permainan
    }
}
