using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBun : MonoBehaviour
{
    public GameObject UiPanel;
    Button startBtn;
    void Awake()
    {
        Time.timeScale = 0;
        startBtn = GetComponent<Button>();
        startBtn.onClick.AddListener(StartGame);
    }


    void StartGame()
    {
        UiPanel.SetActive(false);
        Time.timeScale = 1;
    }
    
}
