using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Febucci.UI;


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
    public Transform balloonAnchor;
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

    private TypewriterByCharacter typewriter;
    private bool isTypingFinished = false;

    private Transform currentSpeakerTransform;

    public bool isInDialogue { get; private set; } = false;

    public Dialogue CurrentDialogue
    {
        get
        {
            if (chapterIndex == 0 || chapterIndex > currentChapterDialogueList.Count)
                return null;
            return currentChapterDialogueList[chapterIndex - 1];
        }
    }

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

        typewriter = dialogueText.GetComponent<TypewriterByCharacter>();
        if (typewriter != null)
        {
            typewriter.onTextShowed.AddListener(OnTypingFinished);
        }

    }

    private void OnTypingFinished()
    {
        isTypingFinished = true;
    }

    private void Update()
    {
        // Test
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    NextDialogue();
        //}
    }

    private void LateUpdate()
    {
        if (isInDialogue && currentSpeakerTransform != null)
        {
            Vector3 worldPos = currentSpeakerTransform.position + new Vector3(0f, 2.5f, 0f);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            dialogueObject.transform.position = screenPos;
        }
    }

    // Input chapter name to argument, Start this chapter's dialogue
    public void StartDialogue(string chapter)
    { 
        PlayerInfoManager.instance.SetIsMoving(false);
        isInDialogue = true;
        FilterDialogueByChapter(chapter);
        dialogueObject.SetActive(true);
        chapterIndex = 0;
        isTypingFinished = true;
        NextDialogue();
    }

    public void EndDialogue()
    {
        PlayerInfoManager.instance.SetIsMoving(true);
        isInDialogue = false;
        dialogueObject.SetActive(false);
        currentSpeakerTransform = null;

        // Save Progress
        PlayerInfoManager.instance.SetProgress(currentChapter, true);
    }

    // Find character who is speaking, Locate speech bubble
    private void SetDialoguelocation(Dialogue currentDialogue)
    {
        Speeker speekerData = allSpeekerList.Find(data => data.name == currentDialogue.character);
        if (speekerData == null || speekerData.character == null)
        {
            Debug.Log("Can't find speaker: " + currentDialogue.character);
            return;
        }

        if (speekerData.balloonAnchor != null)
        {
            currentSpeakerTransform = speekerData.balloonAnchor;
        }
        else
        {
            currentSpeakerTransform = speekerData.character.transform;
        }
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
        if (!isTypingFinished)
        {
            return;
        }

        if (chapterIndex >= currentChapterDialogueList.Count)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = "";

        Dialogue currentDialogue = currentChapterDialogueList[chapterIndex];
        SetDialoguelocation(currentDialogue);

        isTypingFinished = false;

        dialogueText.text = "";
        dialogueText.ForceMeshUpdate();

        typewriter.ShowText(currentDialogue.dialogueText);
        currentDialogue.isDone = true;
        chapterIndex++;
    }

    private void InputText()
    {
        //string _chapter, string _character, string _dialogueText, int _dialogueIndex

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
        allDialogueList.Add(new Dialogue("chapter1.bird03", "nezumi", "<swing>（こんなことで助かるのかな？）", 4, false));

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

        allDialogueList.Add(new Dialogue("chapter2.capy02", "capy", "ゆずのきを探して〜", 0, false));

        allDialogueList.Add(new Dialogue("chapter2.capy03", "capy", "<swing>甘いゆずほしいな～", 0, false)); //capy dialogue if quest is not done yet

        allDialogueList.Add(new Dialogue("chapter2.capy04", "capy", "うわぁ～、おいしそうなゆず！ありがとう！", 0, false));
        allDialogueList.Add(new Dialogue("chapter2.capy04", "capy", "このまま上に進めば、友だちの家があるよ", 1, false));
        allDialogueList.Add(new Dialogue("chapter2.capy04", "capy", "ぼくの友だちの家に入るには", 2, false));
        allDialogueList.Add(new Dialogue("chapter2.capy04", "capy", "ハチミツが大好き", 3, false));
        allDialogueList.Add(new Dialogue("chapter2.capy04", "capy", "って言えばいいんだ", 4, false));
        allDialogueList.Add(new Dialogue("chapter2.capy04", "nezumi", "カピバラさんありがとう！", 5, false));
        #endregion

        #region Chapter 3: Kuma
        allDialogueList.Add(new Dialogue("chapter3.password", "kumaHouse", "パスワード。。。", 0, false));
        allDialogueList.Add(new Dialogue("chapter3.incorrect", "kumaHouse", "違う。。。", 0, false));

        allDialogueList.Add(new Dialogue("chapter3.kuma0", "nezumi", "カピバラさんが", 0, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "nezumi", "クマさんがネズミ村の行き方を知ってる", 1, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "nezumi", "って言ってたよ", 2, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "kuma", "うんうん", 3, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "kuma", "それなら手伝えるかもしれない", 4, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "kuma", "でも、簡単なことじゃないよ", 5, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "nezumi", "何をすればいい？", 6, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "kuma", "ぼくの大好物を持ってきてくれたら", 7, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "kuma", "助けてあげよう", 8, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "nezumi", "<swing>(大好物ってことは……はちみつ、かな？)", 9, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma0", "nezumi", "<swing>（ちょっと探してみよう）", 10, false));

        allDialogueList.Add(new Dialogue("chapter3.kuma1", "kuma", "ぼくの大好きな", 0, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma1", "kuma", "あまいはちみつだ♪", 1, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma1", "nezumi", "やっぱり", 2, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma1", "nezumi", "クマさんの好きなものははちみつだったんだね！", 3, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma1", "kuma", "はちみつをもらったから", 4, false));
        allDialogueList.Add(new Dialogue("chapter3.kuma1", "kuma", "ネズミ村への行き方を教えてあげる", 5, false));


        #endregion

        #region Extra Dialogue


        #endregion

    }


}
