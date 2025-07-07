using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class FinalPart : MonoBehaviour
{
    public static FinalPart Instance;

    [HideInInspector] public List<NPCProfile> chosenProfiles = new List<NPCProfile>();
    [SerializeField] private GameObject marker;
    [SerializeField] private int startX, endX;
    [SerializeField] private float animTime;

    [SerializeField] private Candidate[] candidates;
    private bool doAnim;

    [SerializeField] private GameObject chooseScreen, gameScreen, lastScreen;
    private void Awake()
    {
        doAnim = false;
        Instance = this;
        int i = 0;
        chooseScreen.SetActive(true);
        gameScreen.SetActive(false);
        lastScreen.SetActive(false);
        foreach (var candidate in candidates)
        {
            if (chosenProfiles[i] != null) candidate.SetUpButton(chosenProfiles[i]);
            else candidate.SetEmpty();
        }
    }
    IEnumerator StartAnimationMarker()
    {
        marker.GetComponent<RectTransform>().DOAnchorPosX(endX, animTime).SetLoops(-1);
        yield return new WaitForSeconds(animTime);
        marker.GetComponent<RectTransform>().DOAnchorPosX(startX, animTime).SetLoops(-1);
        yield return new WaitForSeconds(animTime);
        StartCoroutine(StartAnimationMarker());
    }
    public void StopMarker()
    {
        StopCoroutine(StartAnimationMarker());
        // check position
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
