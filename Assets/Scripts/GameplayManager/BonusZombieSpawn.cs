using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusZombieSpawn : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    private int countdownSeconds = 5;
    private int countdownMinutes = 0;

    private float timer = 0;
    private float timeBetweenSpawn = 5;

    public Text timerText;
    public GameObject timeBackground;

    private bool survived = false;
    public GoalManager goalManager;

    // Start is called before the first frame update
    void Start()
    {
        timeBackground.SetActive(true);
        timerText.gameObject.SetActive(true);
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!survived)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenSpawn)
            {
                Spawn();
                timer = 0;
                if (timeBetweenSpawn >= 2f)
                {
                    timeBetweenSpawn -= 0.02f;
                }
            }

            if (countdownMinutes <= 0 && countdownSeconds <= 0)
            {
                Debug.Log("Level complete");
                survived = true;
                goalManager.ZoneSurvived();
            }
        }
        else
        {
            timer = 0;
        }
    }

    private void Spawn()
    {
        int spawn1 = Random.Range(0, 2);
        int spawn2 = Random.Range(3, 5);
        int spawn3 = Random.Range(6, 8);

        var zombie1 = (GameObject)Instantiate(zombiePrefab, spawnPoints[spawn1].position, spawnPoints[spawn1].rotation);
        var zombie2 = (GameObject)Instantiate(zombiePrefab, spawnPoints[spawn2].position, spawnPoints[spawn2].rotation);
        var zombie3 = (GameObject)Instantiate(zombiePrefab, spawnPoints[spawn3].position, spawnPoints[spawn2].rotation);
    }

    private void StartTimer()
    {
        WriteTimer(countdownSeconds, countdownMinutes);
        Invoke(nameof(updateTimer), 1f);
    }

    private void updateTimer()
    {
        countdownSeconds--;
        if (countdownSeconds < 0)
        {
            if (countdownMinutes <= 0)
            {
                timerText.gameObject.SetActive(false);
                timeBackground.SetActive(false);
                goalManager.ZoneSurvived();
                //Invoke(nameof(LoadSurvivedScene), 2f);
            }
            else
            {
                countdownMinutes--;
                countdownSeconds = 59;
            }
        }

        WriteTimer(countdownSeconds, countdownMinutes);
        Invoke(nameof(updateTimer), 1f);
    }

    private void WriteTimer(int seconds, int minutes)
    {
        if (seconds < 10)
        {
            timerText.text = minutes.ToString() + ":0" + seconds;
        }
        else
        {
            timerText.text = minutes.ToString() + ":" + seconds;
        }
    }


}
