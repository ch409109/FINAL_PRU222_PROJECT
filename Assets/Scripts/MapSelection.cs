using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelection : MonoBehaviour
{
    public void SelectMap(int mapId)
    {
        // Store the selected map ID
        PlayerPrefs.SetInt("SelectedMapID", mapId);
        PlayerPrefs.Save();

        // Load the MainScene
        SceneManager.LoadScene(2);
    }
}
