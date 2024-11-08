using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    public void Restart()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void Start()
    {
        resultText.text = GameResult.ResultText; 
    }
}
