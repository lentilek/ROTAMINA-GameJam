using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    private void Start()
    {
        //StartCoroutine(AudioManager.Instance.Vibrations());
        //AudioManager.Instance.Vibrations();
    }
    public void StartGame()
    {
        //AudioManager.Instance.VibrationsStop();
        AudioManager.Instance.ClickSound();
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        AudioManager.Instance.ClickSound();
        Application.Quit();
    }
}
