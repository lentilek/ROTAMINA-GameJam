using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterProfile", menuName = "Scriptable Objects/CharacterProfile")]
public class CharacterProfile : ScriptableObject
{
    public Sprite skin, face, hair, clothes, item;
    public string characterName;
    public OptionSO age, zodiac, gender, personality;
    public List<Likes> likes = new List<Likes>(), dislikes = new List<Likes>();
}

public enum Likes
{
    Travels, Food, Music, Movies, TVSeries, Fishing, Sport, Fashion, Parties, VideoGames, Art, Animals, Books, Nature
}
