using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseScreen;
    public void OnClickPause()
    {
        PauseScreen.SetActive(true);
        PauseGame();
    }

    public void OnClickResume()
    {
        PauseScreen.SetActive(false);
        ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}

