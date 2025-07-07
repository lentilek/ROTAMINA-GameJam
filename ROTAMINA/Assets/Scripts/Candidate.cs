using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Candidate : MonoBehaviour
{
    [SerializeField] private NPCProfile profile;
    [SerializeField] private Image pic;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private Button button;
    [SerializeField] private Sprite emptySprite;

    public void SetUpButton(NPCProfile npc)
    {
        profile = npc;
        pic.sprite = profile.art;
        npcName.text = npc.name;
        button.enabled = true;
    }
    public void SetEmpty()
    {
        button.enabled = false;
        pic.sprite = emptySprite;
        npcName.text = "";
    }
}
