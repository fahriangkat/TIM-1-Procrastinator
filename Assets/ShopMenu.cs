using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
   public GameObject ShopPanel;

   void Update(){

   }
    public void OpenShop(){

        ShopPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Back(){
        ShopPanel.SetActive(false);
        Time.timeScale = 1;
    }
}