using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NPCProfile", menuName = "Scriptable Objects/NPCProfile")]
public class NPCProfile : ScriptableObject
{
    public int npcIndex;
    public Sprite art, profilePic;
    public string characterName;
    public string oneLiner;
    public string introduction;
    public OptionSO age, zodiac, gender, personality;
    public List<Likes> likes = new List<Likes>(), dislikes = new List<Likes>();
    public string startingDialogue;
    public Sprite polaroid;
}
