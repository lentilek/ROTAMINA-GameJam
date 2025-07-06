using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string[] conversationStart;
    public string[] choicesTXT;
    public Likes interstType;
    public int[] optionRelatedIndex;
    public string[] conversationEndOptionsPos;
    public string[] conversationEndOptionsNeutral;
    public string[] conversationEndOptionsNegative;
}
