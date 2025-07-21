using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    public void StartGame()
    {
        AudioManager.Instance.ClickSound();
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        AudioManager.Instance.ClickSound();
        Application.Quit();
    }
}
