using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Candidate : MonoBehaviour
{
    private NPCProfile profile;
    [SerializeField] private Image pic;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private Button button;
    [SerializeField] private Sprite emptySprite;
    private bool canClick;
    public void SetUpButton(NPCProfile npc)
    {
        profile = npc;
        pic.sprite = profile.profilePic;
        npcName.text = npc.characterName;
        canClick = true;
    }
    public void SetEmpty()
    {
        pic.sprite = emptySprite;
        npcName.text = "";
        canClick = false;
    }
    public void Choose()
    {
        if (canClick)
        {
            AudioManager.Instance.ClickSound();
            FinalPart.Instance.StartMArker(profile);
        }
    }
}
