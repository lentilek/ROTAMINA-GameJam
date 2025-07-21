using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
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
    [SerializeField] private Sprite polaroidSolo;
    [SerializeField] private GameObject heart;
    private void Awake()
    {
        doAnim = false;
        Instance = this;
        chooseScreen.SetActive(false);
        gameScreen.SetActive(false);
        lastScreen.SetActive(false);
        heart.SetActive(false);
    }
    public void SetUpStart()
    {
        StartCoroutine(SPecialAnimation(heart, "AnimClip"));
        int i = 0;
        chooseScreen.SetActive(true);
        gameScreen.SetActive(false);
        lastScreen.SetActive(false);
        if (chosenProfiles.Count == 0)
        {
            chooseScreen.SetActive(false);
            lastScreen.SetActive(true);
            polaroid.sprite = polaroidSolo;
            hate.SetActive(true);
        }
        else
        {
            foreach (var candidate in candidates)
            {
                if (i < chosenProfiles.Count) candidate.SetUpButton(chosenProfiles[i]);
                else candidate.SetEmpty();
                i++;
            }
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
        marker.GetComponent<RectTransform>().DOAnchorPosX(endX, animTime);
        yield return new WaitForSeconds(animTime);
        marker.GetComponent<RectTransform>().DOAnchorPosX(startX, animTime);
        yield return new WaitForSeconds(animTime);
        StartCoroutine(StartAnimationMarker());
    }
    public void StopMarker()
    {
        Time.timeScale = 0;
        AudioManager.Instance.ClickSound();
        StopAllCoroutines();
        // check position
        love.SetActive(false);
        hate.SetActive(false);
        friend.SetActive(false);
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
    public IEnumerator SPecialAnimation(GameObject anim, string animationName)
    {
        anim.SetActive(true);
        anim.GetComponent<Animator>().Play(animationName);
        yield return new WaitForSeconds(1f);
        anim.SetActive(false);
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
