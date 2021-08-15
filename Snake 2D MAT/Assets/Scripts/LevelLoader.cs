using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    private Button Button;
    public string SceneName;

    private void Awake()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(SceneName);    
    }
}