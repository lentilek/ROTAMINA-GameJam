using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterProfile", menuName = "Scriptable Objects/CharacterProfile")]
public class CharacterProfile : ScriptableObject
{
    public Image skin, face, hair, clothes, item;
    public string characterName;
    public int ageIndex, zodiacIndex, genderIndex, personalityIndex;
    public bool likesTravels, likesFood, likesMusic, likesMovies, likesTVSeries, likesFishing, likesSport, 
        likesParties, likesFashion, likesVideoGames, likesArt, likesAnimals, likesBooks, likesNature;
    public bool dislikesTravels, dislikesFood, dislikesMusic, dislikesMovies, dislikesTVSeries, dislikesFishing, dislikesSport,
        dislikesParties, dislikesFashion, dislikesVideoGames, dislikesArt, dislikesAnimals, dislikesBooks, dislikesNature;
}

public enum Likes
{
    Travels, Food, Music, Movies, TVSeries, Fishing, Sport, Fashion, Parties, VideoGames, Art, Animals, Books, Nature
}
