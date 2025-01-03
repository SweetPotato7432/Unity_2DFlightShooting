using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Dialog : MonoBehaviour
{
    // ด๋ป็ CSV
    private List<List<string>> csvData = new List<List<string>>();
    private List<string> dialogues = new List<string>();

    public TMP_Text dialogueText;

    public float typingSpeed = 0.05f;

    private string currentDialogue;

    bool coroutineIsRunning = false;
    bool isSkip = false;

    int dialogueNum = 0;

    [SerializeField]
    TutorialManager tutorialManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialManager = FindFirstObjectByType<TutorialManager>();

        CSVLoading csvLoading = new CSVLoading();
        csvData = csvLoading.csvLoad("Dialogues");

        int row_num = 0;
        foreach (List<string> row in csvData)
        {

            dialogues.Add(csvData[row_num][0].ToString());
            row_num++;
        }

        StartDialogue();
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        coroutineIsRunning=true;
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            if (isSkip)
            {
                dialogueText.text =dialogue;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        coroutineIsRunning = false;
        isSkip = false;
        dialogueNum++;
    }

    public void cloaseBtn()
    {
        gameObject.SetActive(false);
    }

    public void ClickPanel()
    {
        if (coroutineIsRunning)
        {
            isSkip = true;
        }
        else
        {
            if(dialogueNum == 1) {
                StartDialogue();
            }
            else if(dialogueNum == 5)
            {
                MySceneManger.Instance.ChangeScene("MainScene");
            }
            else
            {
                tutorialManager.StartTutorialChapter(dialogueNum);
                gameObject.SetActive(false);
            }
            
        }
    }

    public void StartDialogue()
    {
        if (dialogueNum < dialogues.Count)
        {
            currentDialogue = dialogues[dialogueNum];

            StartCoroutine(TypeDialogue(currentDialogue));
        }
    }

    private void OnEnable()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
