using UnityEngine;

[CreateAssetMenu(fileName = "OptionSO", menuName = "Scriptable Objects/OptionSO")]
public class OptionSO : ScriptableObject
{
    public int index;
    public Sprite sprite;
    public string text;
}
