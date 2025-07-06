using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;

public class CharacterCreator : MonoBehaviour
{
    public static CharacterCreator Instance;

    public CharacterProfile charProf;
    [SerializeField] private TextMeshProUGUI charNameTXT;
    [SerializeField] private TMP_Dropdown ageDD, genderDD, zodiacDD, personDD;
    [SerializeField] private OptionSO[] ageOptions, genderOptions, zodiacOptions, personOptions;
    [HideInInspector] public int likesCount, dislikesCount;
    public int makslikesCount, maksdislikesCount, minlikesCount, mindislikesCount;
    private void Awake()
    {
        Instance = this;
        likesCount = 0;
        dislikesCount = 0;
        CharacterProfileReset();
        DropdownSetUp(ageOptions, ageDD);
        DropdownSetUp(genderOptions, genderDD);
        DropdownSetUp(personOptions, personDD);
        DropdownSetUp(zodiacOptions, zodiacDD);
    }
    private void DropdownSetUp(OptionSO[] optionsList, TMP_Dropdown dropdown)
    {
        foreach (OptionSO option in optionsList)
        {
            TMP_Dropdown.OptionData item = new TMP_Dropdown.OptionData();
            item.text = option.text;
            item.image = option.sprite;
            dropdown.options.Add(item);
        }
    }
    public void CharName()
    {
        charProf.characterName = charNameTXT.text;
    }
    public void AgeChanged()
    {
        charProf.age = ageOptions[ageDD.value];
    }
    public void GenderChanged()
    {
        charProf.gender = genderOptions[genderDD.value];
    }
    public void ZodiacChanged()
    {
        charProf.zodiac = zodiacOptions[zodiacDD.value];
    }
    public void PersonChanged()
    {
        charProf.personality = personOptions[personDD.value];
    }
    public void ChangeLike(bool isLike, Likes likeNumber, Toggle toggle)
    {
        if (isLike)
        {
            if (toggle.isOn)
            {
                charProf.likes.Add(likeNumber);
            }
            else
            {
                charProf.likes.Remove(likeNumber);
            }
        }
        else
        {
            if (toggle.isOn)
            {
                charProf.dislikes.Add(likeNumber);
            }
            else
            {
                charProf.dislikes.Remove(likeNumber);
            }
        }
    }
    private void CharacterProfileReset()
    {
        charProf.age = ageOptions[0];
        charProf.gender = genderOptions[0];
        charProf.zodiac = zodiacOptions[0];
        charProf.personality = personOptions[0];
        charProf.characterName = "";

        charProf.likes.Clear();
        charProf.dislikes.Clear();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Continue()
    {
        if (likesCount >= minlikesCount && dislikesCount >= mindislikesCount && charProf.characterName != "")
        {
            StartCoroutine(PhoneAnim());
        }
    }
    [SerializeField] private GameObject phoneScreen, phoneCase;
    [SerializeField] private Vector3 vectorRotate, vectorScale;
    [SerializeField] private float timeAnim;
    [SerializeField] private GameObject swipeUI;
    IEnumerator PhoneAnim()
    {
        phoneScreen.SetActive(false);
        phoneCase.transform.DORotate(vectorRotate, timeAnim);
        phoneCase.transform.DOScale(vectorScale, timeAnim);
        yield return new WaitForSeconds(timeAnim+.3f);
        this.gameObject.SetActive(false);
        swipeUI.SetActive(true);
    }
}

//    Travels, Food, Music, Movies, TVSeries, Fishing, Sport, Parties, Fashion, VideoGames, Art, Animals, Books, Nature
