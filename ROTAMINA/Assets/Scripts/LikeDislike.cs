using UnityEngine;
using UnityEngine.UI;

public class LikeDislike : MonoBehaviour
{
    private Toggle toggle;
    public Likes likeNumber;
    public bool isLike;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }
    private void Update()
    {
        if (isLike)
        {
            if((CharacterCreator.Instance.likesCount >= CharacterCreator.Instance.makslikesCount && !toggle.isOn) ||
                (CharacterCreator.Instance.charProf.dislikes.Contains(likeNumber)))
            {
                toggle.enabled = false;
            }
            else
            {
                toggle.enabled = true;
            }
        }
        else
        {
            if ((CharacterCreator.Instance.dislikesCount >= CharacterCreator.Instance.maksdislikesCount && !toggle.isOn) ||
                (CharacterCreator.Instance.charProf.likes.Contains(likeNumber)))
            {
                toggle.enabled = false;
            }
            else
            {
                toggle.enabled = true;
            }
        }
    }
    public void OnValueChanged()
    {
        if (isLike)
        {
            if (CharacterCreator.Instance.likesCount < CharacterCreator.Instance.makslikesCount)
            {
                if(toggle.isOn) CharacterCreator.Instance.likesCount++;
                else CharacterCreator.Instance.likesCount--;
            }
            else
            {
                if (!toggle.isOn)
                {
                    CharacterCreator.Instance.likesCount--;
                }
            }
        }
        else
        {
            if (CharacterCreator.Instance.dislikesCount < CharacterCreator.Instance.maksdislikesCount)
            {
                if (toggle.isOn) CharacterCreator.Instance.dislikesCount++;
                else CharacterCreator.Instance.dislikesCount--;
            }
            else
            {
                if (!toggle.isOn)
                {
                    CharacterCreator.Instance.dislikesCount--;
                }
            }
        }
        CharacterCreator.Instance.ChangeLike(isLike, likeNumber, toggle);
    }
}
