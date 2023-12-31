using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject PlayerCanvas;
    private IEnumerator coroutine; //refrence to our coroutine
    public GameObject StartMonologue; //the gameobject
    public float seconds = 2f; //our float seconds
    public string[] dialogueSentences;
    public GameObject[] rooms;
    public TextMeshProUGUI enemiesKilledText;
    public int enemiesKilled;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PlayerCanvas.SetActive(false);//we set it false at the start
       PlayerController.instance.enabled = false;//we disable our movement
       StartMonologue.SetActive(true);//we set it false
        StartMonologue.GetComponentInChildren<TextMeshProUGUI>().text = "How did i end up here?I need to find a way out...";
        coroutine = StartGameEnumerator(2f);//we say that the startenumator is equal to coroutine and the time 2 seconds
        StartCoroutine(coroutine);//we call the coroutine
        enemiesKilled = 0;
        enemiesKilledText.text = "Enemies Killed: "+ enemiesKilled.ToString();
        PlaytimeManager.instance.enabled = false;
    }

    IEnumerator StartGameEnumerator(float seconds)
    {
        yield return new WaitForSeconds(seconds);//we wait for specific time
        StartMonologue.SetActive(false);//we disable the gameobject
        PlayerController.instance.enabled = true;//we disable our movement
        PlayerCanvas.SetActive(true);
        PlaytimeManager.instance.enabled = true;
    }
    public void UpdateEnemies()
    {
        enemiesKilled += 1;
        enemiesKilledText.text ="Enemies Killed: " + enemiesKilled.ToString();
    }
}
