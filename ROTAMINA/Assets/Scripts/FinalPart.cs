using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPart : MonoBehaviour
{
    public static FinalPart Instance;

    [HideInInspector] public List<NPCProfile> chosenProfiles = new List<NPCProfile>();
    private void Awake()
    {
        Instance = this;
    }

    public void Click()
    {
        AudioManager.Instance.ClickSound();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
