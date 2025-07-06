using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwipingUI : MonoBehaviour
{
    public static SwipingUI Instance;
    public CharacterProfile charProf;
    [SerializeField] private TextMeshProUGUI charNameTXT;
    [SerializeField] private TextMeshProUGUI ageTXT, genTXT, zodTXT, persTXT, likesTXT;
    [SerializeField] private Image zodiacImage;
    [SerializeField] private Image[] chancesImages;
    [SerializeField] private Sprite[] chancesOptions;

    [SerializeField] private List<NPCProfile> profiles = new List<NPCProfile>();
    private List<NPCProfile> profilesRandom = new List<NPCProfile>();
    [SerializeField] private GameObject npcProfile;
    [SerializeField] private Image npcPic;
    [SerializeField] private TextMeshProUGUI npcDataTXT;
    private void Awake()
    {
        Instance = this;
        PlayerDataIN();
        RandomizeProfiles();
    }
    private void PlayerDataIN()
    {
        charNameTXT.text = charProf.characterName;
        ageTXT.text = charProf.age.text;
        genTXT.text = charProf.gender.text;
        zodTXT.text = charProf.zodiac.text;
        zodiacImage.sprite = charProf.zodiac.sprite;
        persTXT.text = charProf.personality.text;
        string likesText = CreateLikesList(charProf.likes.ToArray());
        string dislikesText = CreateLikesList(charProf.dislikes.ToArray());
        likesTXT.text = $"Likes: {likesText}\nDislikes: {dislikesText}";
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
        npcPic.sprite = profile.art;
        npcDataTXT.text = $"{profile.characterName}, {profile.age.text}";
        profiles.Add(profile);
        profilesRandom.Remove(profile);
    }
    [SerializeField] Vector3 swipePositionLeft, swipeRotate, swipePositionRight;
    [SerializeField] float animTime;
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
        StartCoroutine (SwipeAnimRight());
    }
    IEnumerator SwipeAnimRight()
    {
        RectTransform rt = npcProfile.GetComponent<RectTransform>();
        rt.DOAnchorPos(swipePositionRight, animTime);
        yield return new WaitForSeconds(animTime);
        rt.anchoredPosition = new Vector3(0, 0, 0);
        profiles.RemoveAt(profiles.Count-1);

        // talking

        if (profilesRandom.Count > 0) FillProfile(profilesRandom[Random.Range(0, profilesRandom.Count)]);
        else
        {
            RandomizeProfiles();
        }
    }
}
