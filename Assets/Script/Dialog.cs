using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // 대사 CSV

    public Text dialogueText;

    public float typingSpeed = 0.05f;

    private string currentDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentDialogue = "대사 대사 대사...";

        StartCoroutine(TypeDialogue(currentDialogue));
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void cloaseBtn()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
