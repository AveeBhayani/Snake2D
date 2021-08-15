using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void PlayerDied()
    {
        SoundManager.Instance.Play(Sounds.PlayerDeath);
        gameObject.SetActive(true);
    }

    public void ReloadLevel()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
