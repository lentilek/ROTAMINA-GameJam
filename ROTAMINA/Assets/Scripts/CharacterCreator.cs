using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{
    public static CharacterCreator Instance;

    public CharacterProfile charProf;
    [SerializeField] private TextMeshProUGUI charNameTXT;
    [SerializeField] private TMP_Dropdown ageDD, genderDD, zodiacDD, personDD;
    [HideInInspector] public int likesCount, dislikesCount;
    public int makslikesCount, maksdislikesCount, minlikesCount, mindislikesCount;
    private void Awake()
    {
        Instance = this;
        likesCount = 0;
        dislikesCount = 0;
        CharacterProfileReset();
    }
    public void CharName()
    {
        charProf.characterName = charNameTXT.text;
    }
    public void AgeChanged()
    {
        charProf.ageIndex = ageDD.value;
    }
    public void GenderChanged()
    {
        charProf.genderIndex = genderDD.value;
    }
    public void ZodiacChanged()
    {
        charProf.zodiacIndex = zodiacDD.value;
    }
    public void PersonChanged()
    {
        charProf.personalityIndex = personDD.value;
    }
    public void ChangeLike(bool isLike, Likes likeNumber, Toggle toggle)
    {
        if (isLike)
        {
            switch (likeNumber)
            {
                case Likes.Travels:
                    charProf.likesTravels = toggle.isOn;
                    break;
                case Likes.Food:
                    charProf.likesFood = toggle.isOn;
                    break;
                case Likes.Music:
                    charProf.likesMusic = toggle.isOn;
                    break;
                case Likes.Movies:
                    charProf.likesMovies = toggle.isOn;
                    break;
                case Likes.TVSeries:
                    charProf.likesTVSeries = toggle.isOn;
                    break;
                case Likes.Fishing:
                    charProf.likesFishing = toggle.isOn;
                    break;
                case Likes.Sport:
                    charProf.likesSport = toggle.isOn;
                    break;
                case Likes.Parties:
                    charProf.likesParties = toggle.isOn;
                    break;
                case Likes.Fashion:
                    charProf.likesFashion = toggle.isOn;
                    break;
                case Likes.VideoGames:
                    charProf.likesVideoGames = toggle.isOn;
                    break;
                case Likes.Art:
                    charProf.likesArt = toggle.isOn;
                    break;
                case Likes.Animals:
                    charProf.likesAnimals = toggle.isOn;
                    break;
                case Likes.Books:
                    charProf.likesBooks = toggle.isOn;
                    break;
                case Likes.Nature:
                    charProf.likesNature = toggle.isOn;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (likeNumber)
            {
                case Likes.Travels:
                    charProf.dislikesTravels = toggle.isOn;
                    break;
                case Likes.Food:
                    charProf.dislikesFood = toggle.isOn;
                    break;
                case Likes.Music:
                    charProf.dislikesMusic = toggle.isOn;
                    break;
                case Likes.Movies:
                    charProf.dislikesMovies = toggle.isOn;
                    break;
                case Likes.TVSeries:
                    charProf.dislikesTVSeries = toggle.isOn;
                    break;
                case Likes.Fishing:
                    charProf.dislikesFishing = toggle.isOn;
                    break;
                case Likes.Sport:
                    charProf.dislikesSport = toggle.isOn;
                    break;
                case Likes.Parties:
                    charProf.dislikesParties = toggle.isOn;
                    break;
                case Likes.Fashion:
                    charProf.dislikesFashion = toggle.isOn;
                    break;
                case Likes.VideoGames:
                    charProf.dislikesVideoGames = toggle.isOn;
                    break;
                case Likes.Art:
                    charProf.dislikesArt = toggle.isOn;
                    break;
                case Likes.Animals:
                    charProf.dislikesAnimals = toggle.isOn;
                    break;
                case Likes.Books:
                    charProf.dislikesBooks = toggle.isOn;
                    break;
                case Likes.Nature:
                    charProf.dislikesNature = toggle.isOn;
                    break;
                default:
                    break;
            }
        }
    }
    private void CharacterProfileReset()
    {
        charProf.ageIndex = 0;
        charProf.genderIndex = 0;
        charProf.zodiacIndex = 0;
        charProf.personalityIndex = 0;
        charProf.characterName = "";

        charProf.likesAnimals = false;
        charProf.likesArt = false;
        charProf.likesBooks = false;
        charProf.likesFashion = false;
        charProf.likesFishing = false;
        charProf.likesFood = false;
        charProf.likesMovies = false;
        charProf.likesMusic = false;
        charProf.likesNature = false;
        charProf.likesParties = false;
        charProf.likesSport = false;
        charProf.likesTravels = false;
        charProf.likesTVSeries = false;
        charProf.likesVideoGames = false;

        charProf.dislikesAnimals = false;
        charProf.dislikesArt = false;
        charProf.dislikesBooks = false;
        charProf.dislikesFashion = false;
        charProf.dislikesFishing = false;
        charProf.dislikesFood = false;
        charProf.dislikesMovies = false;
        charProf.dislikesMusic = false;
        charProf.dislikesNature = false;
        charProf.dislikesParties = false;
        charProf.dislikesSport = false;
        charProf.dislikesTravels = false;
        charProf.dislikesTVSeries = false;
        charProf.dislikesVideoGames = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Continue()
    {
        if (likesCount > minlikesCount && dislikesCount > mindislikesCount)
        {
            this.gameObject.SetActive(false);        
            //SwipingUI.gameObject.SetActive(true);
        }
    }
}

//    Travels, Food, Music, Movies, TVSeries, Fishing, Sport, Parties, Fashion, VideoGames, Art, Animals, Books, Nature
