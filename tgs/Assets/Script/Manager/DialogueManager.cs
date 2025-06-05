using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string chapter;
    public string character;
    public string dialogueText;
    public int dialogueIndex;
    public bool isDone;

    //Constructor
    public Dialogue(string _chapter, string _character, string _dialogueText, int _dialogueIndex, bool _isDone)
    {
        chapter = _chapter;
        character = _character;
        dialogueText = _dialogueText;
        dialogueIndex = _dialogueIndex;
        isDone = _isDone;

    }
}

[System.Serializable]
public class Speeker
{
    public string name;
    public GameObject character;
}

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    private DialogueManager()
    {
        
    }

    public static DialogueManager GetInstance()
    {
        if (instance == null)
        {
            instance = new DialogueManager();
        }
        return instance;
    }

    //-------------------------------------------------------------

    public GameObject dialogueObject;
    public List<Dialogue> allDialogueList;
    public List<Speeker> allSpeekerList;

    public TextMeshProUGUI dialogueText;

    private int index = 0;
    private GameObject speeker;
   

    private void Start()
    {
        dialogueText = dialogueObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        InputText();
    }

    public void StartDialogue()
    {
        dialogueObject.SetActive(true);
    }

    public void EndDialogue()
    {
        dialogueObject.SetActive(false);
    }

    private void InputText()
    {
        //string _chapter, string _character, string _dialogueText, int _dialogueIndex
        allDialogueList.Add(new Dialogue("bird", "nezumi", "Butterfly!", 0, false));
        allDialogueList.Add(new Dialogue("bird", "bird", "hey", 1, false));
    }

    IEnumerator TypingEffect(TextMeshProUGUI targetText, string text)
    {
        targetText.text = string.Empty;

        StringBuilder stringBuilder = new StringBuilder();

        for (int i=0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);
            targetText.text = stringBuilder.ToString();

            var dialogueindexData = allDialogueList.Find(data => data.dialogueIndex == index);
            if (dialogueindexData != null)
            {
                dialogueindexData.isDone = true;
            }

        }

        yield return new WaitForSeconds(0.1f);
    }

    private void FindSpeaker(string chapter)
    {
        if (chapter == "bird")
        {

        }
    }

    private void FindSpeeker()
    {
    }

    private void SetDialoguelocation()
    {
        //var speekerdata =
    }

}
