using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    private int totalGoals = 2;
    private int goalsCompleted = 0;

    private bool survivedZone = false;

    private int totalGems = 12;
    private int currentGemsDestroyed = 0;

    public TextMeshProUGUI mission1Text;
    public TextMeshProUGUI gemCounter;
    public TextMeshProUGUI mission2Text;

    public GameObject fadeInWin;

    public AudioSource rockSong;
    public AudioSource gameSong;

    public void GemDestroyed()
    {
        currentGemsDestroyed++;
        gemCounter.text = currentGemsDestroyed + "/12";
        if (currentGemsDestroyed >= totalGems)
        {
            mission1Text.color = Color.green;
            gemCounter.color = Color.green;
            goalsCompleted++;
            Debug.Log("Mission Accomplished");
            if (goalsCompleted == 2)
            {
                GameWin();
            }
        }
    }

    public void ZoneSurvived()
    {
        if (!survivedZone)
        {
            rockSong.Stop();
            gameSong.Play();
            survivedZone = true;
            goalsCompleted++;
            mission2Text.color = Color.green;
            Debug.Log("Mission Accomplished");
            if (goalsCompleted == 2)
            {
                GameWin();
            }
        }
    }

    private void GameWin()
    {
        Debug.Log("You win the game!");
        fadeInWin.SetActive(true);
        Invoke(nameof(LoadWinScene), 1f);
    }

    private void LoadWinScene()
    {
        SceneManager.LoadScene("YouSurvivedScene");
    }
}
