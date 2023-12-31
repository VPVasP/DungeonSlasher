using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaytimeManager : MonoBehaviour
{
    public static PlaytimeManager instance;
    private float currentplayTime;
    public TextMeshProUGUI playtimeText;
    private void Awake()
    {
        instance = this;
    }
   
    private void Update()
    {
        currentplayTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentplayTime / 60);
        int seconds = Mathf.FloorToInt(currentplayTime % 60);
        string currentTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        playtimeText.text = "Current Playtime: " + currentTime;
    }
   
 
}
