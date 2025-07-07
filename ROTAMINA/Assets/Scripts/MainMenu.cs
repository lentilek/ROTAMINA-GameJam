using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
