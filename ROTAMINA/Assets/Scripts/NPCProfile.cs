using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NPCProfile", menuName = "Scriptable Objects/NPCProfile")]
public class NPCProfile : ScriptableObject
{
    public int npcIndex;
    public Image art;
    public string characterName;
    public int ageIndex, zodiacIndex, genderIndex, personalityIndex;
    public bool likesTravels, likesFood, likesMusic, likesMovies, likesTVSeries, likesFishing, likesSport,
        likesParties, likesFashion, likesVideoGames, likesArt, likesAnimals, likesBooks, likesNature;
    public bool dislikesTravels, dislikesFood, dislikesMusic, dislikesMovies, dislikesTVSeries, dislikesFishing, dislikesSport,
        dislikesParties, dislikesFashion, dislikesVideoGames, dislikesArt, dislikesAnimals, dislikesBooks, dislikesNature;
}
