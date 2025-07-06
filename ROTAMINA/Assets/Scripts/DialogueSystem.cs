using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    [Header("Dialogues Configs")]
    [SerializeField] private string[] startDialogues;
    [SerializeField] private Dialogue[] interestDialogues;
    [SerializeField] private Dialogue[] typeDialogues;
    [SerializeField] private string[] endDialoguesPos, endDialoguesNeg;

    [Header("Objects")]
    [SerializeField] private GameObject dialogueStart, dialogueStart2, dialogueEnd;
    [SerializeField] private GameObject[] dialogueOptions;
    [SerializeField] private TextMeshProUGUI dialogueStartTXT, dialogueStart2TXT, dialogueEndTXT;
    [SerializeField] private TextMeshProUGUI[] dialogueOptionsTXT;
    [SerializeField] private GameObject continueDialogueButton;

    private Dialogue currentDialogue;
    [HideInInspector] public NPCProfile npc;
    private int stage;
    private int points;
    private int interestIndex, typeIndex;
    private void Awake()
    {
        Instance = this;
    }
    public IEnumerator ConversationStart()
    {
        points = 0;
        stage = 0;
        foreach (GameObject go in dialogueOptions)
        {
            go.GetComponent<CanvasGroup>().alpha = 0f;
        }
        dialogueEnd.GetComponent<CanvasGroup>().alpha = 0f;
        continueDialogueButton.SetActive(false);
        dialogueStart.GetComponent<CanvasGroup>().alpha = 0f;
        dialogueStartTXT.text = npc.startingDialogue;
        dialogueStart2.GetComponent<CanvasGroup>().alpha = 0f;
        dialogueStart.GetComponent<CanvasGroup>().DOFade(1, .5f);
        yield return new WaitForSeconds(.5f);
        
        dialogueStart2TXT.text = startDialogues[Random.Range(0, startDialogues.Length)];
        dialogueStart2.GetComponent<CanvasGroup>().DOFade(1, .5f);
        yield return new WaitForSeconds(.5f);
        continueDialogueButton.SetActive(true);
    }
    public void Continue()
    {
        switch (stage)
        {
            case 0:
                dialogueStart2.GetComponent<CanvasGroup>().alpha = 0f;
                StartCoroutine(InitiateConvesration(interestDialogues[Random.Range(0, interestDialogues.Length)])); 
                break;
            case 1:
                StartCoroutine(InitiateConvesration(typeDialogues[Random.Range(0, typeDialogues.Length)]));
                break;
            case 2:
                StartCoroutine(InitiateConvesration(interestDialogues[Random.Range(0, interestDialogues.Length)]));
                break;
            case 3:
                StartCoroutine(InitiateConvesration(typeDialogues[Random.Range(0, typeDialogues.Length)]));
                break;
            case 4:
                dialogueEnd.GetComponent<CanvasGroup>().alpha = 0f;
                foreach (GameObject go in dialogueOptions)
                {
                    go.GetComponent<CanvasGroup>().alpha = 0f;
                }
                if (points >= 3)
                {
                    StartCoroutine(EndTalk(endDialoguesPos[Random.Range(0, endDialoguesPos.Length)]));
                }
                else
                {
                    StartCoroutine(EndTalk(endDialoguesPos[Random.Range(0, endDialoguesPos.Length)]));
                }
                stage++;
                break;
            case 5:
                SwipingUI.Instance.ContinueGame();
                break;
            default: break;
        }
    }
    private IEnumerator EndTalk(string text)
    {
        continueDialogueButton.SetActive(false);
        dialogueStart.GetComponent<CanvasGroup>().alpha = 0f;
        dialogueStartTXT.text = text;
        dialogueStart2.GetComponent<CanvasGroup>().alpha = 0f;
        dialogueStart.GetComponent<CanvasGroup>().DOFade(1, .5f);
        yield return new WaitForSeconds(.5f);
        continueDialogueButton.SetActive(true);
    }
    private IEnumerator InitiateConvesration(Dialogue dialogue)
    {
        stage++;
        continueDialogueButton.SetActive(false);
        currentDialogue = dialogue;
        dialogueStart.GetComponent<CanvasGroup>().alpha = 0f;
        dialogueEnd.GetComponent<CanvasGroup>().alpha = 0f;
        foreach (GameObject go in dialogueOptions)
        {
            go.GetComponent<CanvasGroup>().alpha = 0f;
        }
        dialogueStartTXT.text = dialogue.conversationStart[0];
        dialogueStart.GetComponent<CanvasGroup>().DOFade(1, .5f);
        yield return new WaitForSeconds(.5f);
        int i = 0;
        foreach (GameObject go in dialogueOptions)
        {
            dialogueOptionsTXT[i].text = dialogue.choicesTXT[i];
            go.GetComponent<CanvasGroup>().DOFade(1, .5f);
            i++;
        }
    }
    public void DialogueOption(int index)
    {
        for(int i = 0; i < currentDialogue.choicesTXT.Length; i++)
        {
            if (i != index) dialogueOptions[i].GetComponent<CanvasGroup>().DOFade(0, .3f);
        }
        /// count points
        int indexEnd = 0;
        dialogueEndTXT.text = currentDialogue.conversationEndOptionsPos[indexEnd];
        dialogueEnd.GetComponent<CanvasGroup>().DOFade(1, .5f);
        continueDialogueButton.SetActive(true);
    }
}
