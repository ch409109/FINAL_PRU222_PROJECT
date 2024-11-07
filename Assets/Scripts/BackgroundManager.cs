using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Sprite[] mapSprites;

    void Start()
    {
        int selectedMap = PlayerPrefs.GetInt("SelectedMapID", 0);
        GetComponent<SpriteRenderer>().sprite = mapSprites[selectedMap];
    }
}
