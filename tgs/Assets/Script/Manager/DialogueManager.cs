using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
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
    public static DialogueManager instance { get; private set; }


    public GameObject dialogueObject;
    public TextMeshProUGUI dialogueText;

    public List<Dialogue> allDialogueList;
    public List<Speeker> allSpeekerList;

    private string currentChapter;
    private List<Dialogue> currentChapterDialogueList = new List<Dialogue>();
    private int chapterIndex = 0;

    public bool isInDialogue { get; private set; } = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        // Connect with TMP UI
        dialogueText = dialogueObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        // Set allDialogueList
        InputText();

        // TestCode
        //StartDialogue("bird");

    }

    private void Update()
    {
        // Test
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    NextDialogue();
        //}
    }

    // Input chapter name to argument, Start this chapter's dialogue
    public void StartDialogue(string chapter)
    {
        isInDialogue = true;
        FilterDialogueByChapter(chapter);
        dialogueObject.SetActive(true);
        chapterIndex = 0;
        NextDialogue();
    }

    public void EndDialogue()
    {
        isInDialogue = false;
        dialogueObject.SetActive(false);

        // Save Progress
        PlayerInfoManager.instance.SetProgress(currentChapter, true);
    }

    //IEnumerator TypingEffect(TextMeshProUGUI targetText, string text)
    //{
    //    targetText.text = string.Empty;
    //    StringBuilder stringBuilder = new StringBuilder();

    //    for (int i = 0; i < text.Length; i++)
    //    {
    //        stringBuilder.Append(text[i]);
    //        targetText.text = stringBuilder.ToString();
    //        yield return new WaitForSeconds(0.05f);
    //    }

    //    var current = currentChapterDialogueList[chapterIndex - 1];
    //    current.isDone = true;
    //}
    
    // Find character who is speaking, Locate speech bubble
    private void SetDialoguelocation(Dialogue currentDialogue)
    {
        Speeker speekerData = allSpeekerList.Find(data => data.name == currentDialogue.character);
        if (speekerData == null || speekerData.character == null)
        {
            Debug.Log("Can't find speeker : " + currentDialogue.character);
            return;
        }

        Vector3 speakerWorldPosition = speekerData.character.transform.position + new Vector3(0f, 2.5f, 0f);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(speakerWorldPosition);
        dialogueObject.transform.position = screenPosition;
    }


    private void FilterDialogueByChapter(string chapter)
    {
        currentChapter = chapter;
        currentChapterDialogueList = allDialogueList.FindAll(d => d.chapter == chapter);

        currentChapterDialogueList.Sort((a, b) => a.dialogueIndex.CompareTo(b.dialogueIndex));

        chapterIndex = 0;
    }

    public void NextDialogue()
    {
        if (chapterIndex >= currentChapterDialogueList.Count)
        {
            EndDialogue();
            return;
        }

        Dialogue currentDialogue = currentChapterDialogueList[chapterIndex];

        SetDialoguelocation(currentDialogue);

        StopAllCoroutines();
        //StartCoroutine(TypingEffect(dialogueText, currentDialogue.dialogueText));
        dialogueText.text = currentDialogue.dialogueText;
        currentDialogue.isDone = true;

        chapterIndex++;
    }

    private void InputText()
    {
        //string _chapter, string _character, string _dialogueText, int _dialogueIndex

        //Test
        #region TestDialogue
        allDialogueList.Add(new Dialogue("chapter1.bird01", "nezumi", "<wave>Butterfly!", 0, false));
        allDialogueList.Add(new Dialogue("chapter1.bird01", "bird", "hey", 1, false));
        allDialogueList.Add(new Dialogue("chapter1.bird01", "bird", "nezumi!", 2, false));
        allDialogueList.Add(new Dialogue("chapter1.bird01", "nezumi", "why?", 3, false));

        allDialogueList.Add(new Dialogue("chapter1.bird02", "bird", "find branch", 0, false));
        #endregion
    }


}
