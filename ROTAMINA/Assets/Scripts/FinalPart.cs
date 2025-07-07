using System.Collections.Generic;
using UnityEngine;

public class FinalPart : MonoBehaviour
{
    public static FinalPart Instance;

    [HideInInspector] public List<NPCProfile> chosenProfiles = new List<NPCProfile>();
    private void Awake()
    {
        Instance = this;
    }
}
