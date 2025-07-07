using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalPart : MonoBehaviour
{
    public static FinalPart Instance;

    [HideInInspector] public List<NPCProfile> chosenProfiles = new List<NPCProfile>();
    private NPCProfile choosen;
    [SerializeField] private GameObject marker;
    private float startX, endX;
    [SerializeField] private float movement;
    [SerializeField] private float animTime;

    [SerializeField] private Candidate[] candidates;
    private bool doAnim;

    [SerializeField] private GameObject chooseScreen, gameScreen, lastScreen;

    [SerializeField] private float friendMin, friendMax, romanceMin, romanceMax;
    [SerializeField] private float friendMinAdd, friendMaxAdd, romanceMinAdd, romanceMaxAdd;
    [SerializeField] private GameObject friend, love, hate;
    [SerializeField] private Image polaroid;
    private void Awake()
    {
        doAnim = false;
        Instance = this;
        chooseScreen.SetActive(false);
        gameScreen.SetActive(false);
        lastScreen.SetActive(false);
    }
    public void SetUpStart()
    {
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
    public void StartMArker(NPCProfile npc)
    {
        startX = marker.GetComponent<RectTransform>().anchoredPosition.x;
        endX = startX + movement;
        choosen = npc;
        gameScreen.SetActive(true);
        chooseScreen.SetActive(false);
        StartCoroutine(StartAnimationMarker());
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
        AudioManager.Instance.ClickSound();
        StopCoroutine(StartAnimationMarker());
        // check position
        friendMinAdd = startX + friendMin;
        friendMaxAdd = startX + friendMax;
        romanceMinAdd = startX + romanceMin;
        romanceMaxAdd = startX + romanceMax;
        float currentX = marker.GetComponent<RectTransform>().anchoredPosition.x;
        if (currentX >= romanceMinAdd && currentX <= romanceMaxAdd)
        {
            love.SetActive(true);
        } else if (currentX >= friendMinAdd && currentX <= friendMaxAdd)
        {
            friend.SetActive(true);
        }
        else
        {
            hate.SetActive(true);
        }
        polaroid.sprite = choosen.polaroid;
        lastScreen.SetActive(true);
        gameScreen.SetActive(false);
    }
    public void Click()
    {
        AudioManager.Instance.ClickSound();
    }
    public void MainMenu()
    {
        AudioManager.Instance.ClickSound();
        SceneManager.LoadScene(0);
    }
}
