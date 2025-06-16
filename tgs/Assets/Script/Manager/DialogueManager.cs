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
        //allDialogueList.Add(new Dialogue("chapter1.bird01", "nezumi", "<wave>Butterfly!", 0, false));
        //allDialogueList.Add(new Dialogue("chapter1.bird01", "bird", "hey", 1, false));
        //allDialogueList.Add(new Dialogue("chapter1.bird01", "bird", "nezumi!", 2, false));
        //allDialogueList.Add(new Dialogue("chapter1.bird01", "nezumi", "why?", 3, false));

        //allDialogueList.Add(new Dialogue("chapter1.bird02", "bird", "find branch", 0, false));

        //allDialogueList.Add(new Dialogue("questComplete", "bird", "thank you", 0, false));
        #endregion

        #region Chapter 1: Bird
        //allDialogueList.Add(new Dialogue("hey", "bird", "<wave>やあ！ネズミちゃん！", 0, false));

        allDialogueList.Add(new Dialogue("chapter1.bird01", "bird", "ここで君を見るのは初めてだね。迷子かな？", 0, false));
        allDialogueList.Add(new Dialogue("chapter1.bird01", "nezumi", "うん、たぶん。助けてくれる？", 1, false));
        allDialogueList.Add(new Dialogue("chapter1.bird01", "bird", "んー、欲しいものくれたら手伝ってあげるよ！", 2, false));
        allDialogueList.Add(new Dialogue("chapter1.bird01", "nezumi", "何ほしいの？", 3, false));
        allDialogueList.Add(new Dialogue("chapter1.bird01", "bird", "巣作ってるんだ！", 4, false));
        allDialogueList.Add(new Dialogue("chapter1.bird01", "bird", "あと枝が一本だけ欲しいんだ！見つけてくれたら助けるね", 5, false));
        allDialogueList.Add(new Dialogue("chapter1.bird01", "nezumi", "わかった！", 6, false));

        allDialogueList.Add(new Dialogue("chapter1.bird02", "bird", "枝見つけた？", 0, false));

        allDialogueList.Add(new Dialogue("chapter1.bird03", "bird", "やったね！欲しかった枝だ！？", 0, false)); //枝渡した後
        allDialogueList.Add(new Dialogue("chapter1.bird03", "bird", "じゃーー", 1, false));
        allDialogueList.Add(new Dialogue("chapter1.bird03", "bird", "この道進めば、他にも家まで案内してくれる動物がいるよ", 2, false));
        allDialogueList.Add(new Dialogue("chapter1.bird03", "bird", "笑", 3, false));
        allDialogueList.Add(new Dialogue("chapter1.bird03", "nezumi", "・・・（こんなことで助かるのかな？）", 4, false));
        allDialogueList.Add(new Dialogue("chapter1.bird03", "nezumi", "<swing>（こんなことで助かるのかな？）", 5, false));

        allDialogueList.Add(new Dialogue("chapter1.bird04", "bird", "なに？", 0, false));
        #endregion

        #region Chapter 2: Capy
        allDialogueList.Add(new Dialogue("chapter2.capy00", "nezumi", "（ここに助けてくれる人がいるかも。ちょっと聞いてみよう。）", 0, false)); //nezu chan自分で自動的に話す

        allDialogueList.Add(new Dialogue("chapter2.capy01", "nezumi", "こんにちは！ちょっと手伝ってくれる？", 0, false)); //dialogue with capy start
        allDialogueList.Add(new Dialogue("chapter2.capy01", "nezumi", "ネズミ村に帰る道教えてほしい！", 1, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "capy", "ネズミ村か。。", 2, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "capy", "ごめんね、ぼくもよくわからないけど、。。", 3, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "capy", "ぼうけんしゃの友だちが知ってるかもよ～", 4, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "nezumi", "ぼうけんしゃの友だち？", 5, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "capy", "うん！あっちの家に住んでるけど、", 6, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "capy", "いつもかぎがかかってるらしい。。", 7, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "nezumi", "そうか。。", 8, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "capy", "でね！", 9, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "capy", "甘いゆずくれたら、その家に入るひみつを教えてあげるかな～", 10, false));
        allDialogueList.Add(new Dialogue("chapter2.capy01", "nezumi", "ゆずか〜。さがしてみよっかな。）", 11, false));

        allDialogueList.Add(new Dialogue("chapter2.capy02", "capy", "<swing>甘いゆずほしいな～", 0, false)); //capy dialogue if quest is not done yet
        #endregion

        #region Chapter 3: Kuma

        #endregion

    }


}
