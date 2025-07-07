using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwipingUI : MonoBehaviour
{
    public static SwipingUI Instance;

    [Header("Player data")]
    public CharacterProfile charProf;
    [SerializeField] private Image profilePic;
    [SerializeField] private TextMeshProUGUI charNameTXT;
    [SerializeField] private TextMeshProUGUI ageTXT, genTXT, zodTXT, persTXT, likesTXT;
    [SerializeField] private Image zodiacImage;
    [SerializeField] private Image[] chancesImages;
    [SerializeField] private Sprite[] chancesOptions;

    [Header("NPC profile")]
    [SerializeField] private List<NPCProfile> profiles = new List<NPCProfile>();
    private List<NPCProfile> profilesRandom = new List<NPCProfile>();
    [SerializeField] private GameObject npcProfile;
    [SerializeField] private Image npcPic;
    [SerializeField] private TextMeshProUGUI npcDataTXT;
    [SerializeField] private TextMeshProUGUI npcOneLinerTXT;

    [Header("Swipe animation")]
    [SerializeField] private Vector3 swipePositionLeft;
    [SerializeField] private Vector3 swipeRotate;
    [SerializeField] private Vector3 swipePositionRight;
    [SerializeField] private float animTime;

    [Header("Prepare messages")]
    [SerializeField] private GameObject phoneSwiping;
    [SerializeField] private GameObject phoneMessages;
    [SerializeField] private GameObject npcData;

    [Header("NPC data")]
    [SerializeField] private TextMeshProUGUI charNameTXTNPC;
    [SerializeField] private TextMeshProUGUI ageTXTNPC, genTXTNPC, zodTXTNPC, persTXTNPC, likesTXTNPC;
    [SerializeField] private Image zodiacImageNPC;
    [SerializeField] private Image profilePicNPC;
    [HideInInspector] public List<NPCProfile> chosenProfiles = new List<NPCProfile>();
    [SerializeField] private GameObject npcDataAll;

    private NPCProfile currentProfile;
    [HideInInspector] public int chancesUsed;
    [SerializeField] private GameObject finalPart;

    public GameObject nope, matched, unmatched, finalmatch;
    private void Awake()
    {
        Instance = this;
        chancesUsed = 0;
        phoneSwiping.SetActive(true);
        phoneMessages.SetActive(false);
        npcData.SetActive(false);
        nope.SetActive(false);
        matched.SetActive(false);
        unmatched.SetActive(false);
        finalmatch.SetActive(false);
        PlayerDataIN();
        RandomizeProfiles();
    }
    private void PlayerDataIN()
    {
        profilePic.sprite = charProf.profilePic;
        charNameTXT.text = charProf.characterName;
        ageTXT.text = charProf.age.text;
        genTXT.text = charProf.gender.text;
        zodTXT.text = charProf.zodiac.text;
        zodiacImage.sprite = charProf.zodiac.sprite;
        persTXT.text = charProf.personality.text;
        string likesText = CreateLikesList(charProf.likes.ToArray());
        string dislikesText = CreateLikesList(charProf.dislikes.ToArray());
        likesTXT.text = $"Likes: {likesText}\n\nDislikes: {dislikesText}";
        foreach (Image image in chancesImages)
        {
            image.sprite = chancesOptions[0];
        }
    }
    private string CreateLikesList(Likes[] likes)
    {
        string text = "";
        foreach(Likes like in likes)
        {
            text += $"{like.ToString()}, ";
        }
        return text;
    }
    private void RandomizeProfiles()
    {
        int length = profiles.Count;
        NPCProfile profile;
        for(int i = 0; i < length; i++)
        {
            profile = profiles[Random.Range(0, profiles.Count)];
            profilesRandom.Add(profile);
            profiles.Remove(profile);
        }
        FillProfile(profilesRandom[Random.Range(0, profilesRandom.Count)]);
    }
    private void FillProfile(NPCProfile profile)
    {
        currentProfile = profile;
        npcPic.sprite = profile.art;
        npcDataTXT.text = $"{profile.characterName}, {profile.age.text}";
        npcOneLinerTXT.text = profile.oneLiner;
        profiles.Add(profile);
        profilesRandom.Remove(profile);
    }
    public void LeftButton()
    {
        StartCoroutine(SwipeAnimLeft());
    }
    IEnumerator SwipeAnimLeft()
    {
        RectTransform rt = npcProfile.GetComponent<RectTransform>();
        rt.DOAnchorPos(swipePositionLeft, animTime);
        yield return new WaitForSeconds(animTime);
        rt.anchoredPosition = new Vector3(0, 0, 0);
        if (profilesRandom.Count > 0) FillProfile(profilesRandom[Random.Range(0, profilesRandom.Count)]);
        else
        {
            RandomizeProfiles();
        }
    }
    public void RightButton()
    {
        chancesUsed++;
        StartCoroutine (SwipeAnimRight());
    }
    IEnumerator SwipeAnimRight()
    {
        RectTransform rt = npcProfile.GetComponent<RectTransform>();
        rt.DOAnchorPos(swipePositionRight, animTime);
        yield return new WaitForSeconds(animTime);
        rt.anchoredPosition = new Vector3(0, 0, 0);

        int index = profiles.Count - 1;
        chosenProfiles.Add(profiles[index]);
        profiles.RemoveAt(index);

        // matching
        if (CheckIfMatched(chosenProfiles[chosenProfiles.Count - 1]))
        {
            AudioManager.Instance.PlaySound("matched");
            StartCoroutine(SPecialAnimation(matched, "Match"));
            phoneMessages.SetActive(true);
            phoneSwiping.SetActive(false);
            npcData.SetActive(true);
            NPCDataIN(chosenProfiles.Count - 1);
        }
        else
        {
            AudioManager.Instance.PlaySound("nope");
            StartCoroutine(SPecialAnimation(nope, "nope"));
            ChanceImage(false);
            ContinueGame();
        }
    }
    private bool CheckIfMatched(NPCProfile npc)
    {
        int tempPoints = 0;
        foreach (Likes like in npc.likes)
        {
            if (charProf.likes.Contains(like)) tempPoints++;
            else if (charProf.dislikes.Contains(like)) tempPoints--;
        }
        foreach (Likes dislike in npc.dislikes)
        {
            if (charProf.dislikes.Contains(dislike)) tempPoints++;
            else if (charProf.likes.Contains(dislike)) tempPoints--;
        }
        if (npc.personality.index == charProf.personality.index) tempPoints++;
        else if ((npc.personality.index == 0 && charProf.personality.index == 3) || 
            (npc.personality.index == 3 && charProf.personality.index == 0) ||
            (npc.personality.index == 1 && charProf.personality.index == 2) ||
            (npc.personality.index == 2 && charProf.personality.index == 1))
        {
            tempPoints--;
        }
        if (tempPoints >= 0) return true;
        else return false;
    }
    public void HoverShowProfile()
    {
        charNameTXTNPC.text = currentProfile.characterName;
        profilePicNPC.sprite = currentProfile.profilePic;
        ageTXTNPC.text = currentProfile.age.text;
        genTXTNPC.text = currentProfile.gender.text;
        zodTXTNPC.text = currentProfile.zodiac.text;
        zodiacImageNPC.sprite = currentProfile.zodiac.sprite;
        persTXTNPC.text = currentProfile.personality.text;
        string likesText = CreateLikesList(currentProfile.likes.ToArray());
        string dislikesText = CreateLikesList(currentProfile.dislikes.ToArray());
        likesTXTNPC.text = $"Likes: {likesText}\n\nDislikes: {dislikesText}";
        npcDataAll.SetActive(true);
    }
    public void HideProfile()
    {
        npcDataAll.SetActive(false);
    }
    private void NPCDataIN(int index)
    {
        charNameTXTNPC.text = chosenProfiles[index].characterName;
        profilePicNPC.sprite = chosenProfiles[index].profilePic;
        ageTXTNPC.text = chosenProfiles[index].age.text;
        genTXTNPC.text = chosenProfiles[index].gender.text;
        zodTXTNPC.text = chosenProfiles[index].zodiac.text;
        zodiacImageNPC.sprite = chosenProfiles[index].zodiac.sprite;
        persTXTNPC.text = chosenProfiles[index].personality.text;
        string likesText = CreateLikesList(chosenProfiles[index].likes.ToArray());
        string dislikesText = CreateLikesList(chosenProfiles[index].dislikes.ToArray());
        likesTXTNPC.text = $"Likes: {likesText}\n\nDislikes: {dislikesText}";
        DialogueSystem.Instance.npc = chosenProfiles[index];
        npcDataAll.SetActive(true);
        StartCoroutine(DialogueSystem.Instance.ConversationStart());
    }
    public void ChanceImage(bool positive)
    {
        if (positive) chancesImages[chancesUsed - 1].sprite = chancesOptions[2];
        else chancesImages[chancesUsed - 1].sprite = chancesOptions[1];
    }
    public void ContinueGame()
    {
        if (chancesUsed != 4)
        {
            phoneSwiping.SetActive(true);
            phoneMessages.SetActive(false);
            npcData.SetActive(false);
            if (profilesRandom.Count > 0) FillProfile(profilesRandom[Random.Range(0, profilesRandom.Count)]);
            else
            {
                RandomizeProfiles();
            }
        }
        else
        {
            finalPart.SetActive(true);
            FinalPart.Instance.chosenProfiles = chosenProfiles;
            this.gameObject.SetActive(false);
        }
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
        SceneManager.LoadScene(0);
    }
}
