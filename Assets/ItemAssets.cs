using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour {

    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }


    public Transform pfItemWorld;

    public Sprite basiliskVenomSprite;
    public Sprite magicapSprite;
    public Sprite dracoWingsSprite;
    public Sprite coinSprite;
    public Sprite fireFungusSprite;


}