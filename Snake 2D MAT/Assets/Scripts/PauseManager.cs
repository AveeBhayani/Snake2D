using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseScreen;
    public void OnClickPause()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        PauseScreen.SetActive(true);
        PauseGame();
    }

    public void OnClickResume()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
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
