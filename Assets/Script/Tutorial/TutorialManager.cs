using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    Dialog dialog;

    [SerializeField]
    GameObject varialbeJoystick;
    [SerializeField]
    GameObject atkButton;
    [SerializeField]
    bool isCorutineStart = false;

    bool isButtonPress = false;

    int currentChapter = 0;

    Vector3 currentPlayerPos;

    private Coroutine tutorialCorutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        gameManager = FindFirstObjectByType<GameManager>();

        dialog = FindFirstObjectByType<Dialog>();
        //dialog.gameObject.SetActive(true);


        gameManager.TutorialStart();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentChapter)
        {
            case 2:
                if (!isCorutineStart && 
                    currentPlayerPos != player.transform.position &&
                    tutorialCorutine == null)
                {
                    isCorutineStart=true;
                    tutorialCorutine = StartCoroutine(WaitforNextChapter());
                }
                break;
            case 3:

                if (!isCorutineStart &&
                    isButtonPress &&
                    tutorialCorutine == null)
                {
                    isCorutineStart = true;
                    tutorialCorutine = StartCoroutine(WaitforNextChapter());
                }

                break;
            case 4:
                if (!isCorutineStart &&
                    gameManager.GetScore() == 200 &&
                    tutorialCorutine == null)
                {
                    isCorutineStart = true;
                    dialog.gameObject.SetActive(true);
                }
                break;
        }
    }

    IEnumerator WaitforNextChapter()
    {
        Debug.Log("ÄÚ·çÆ¾");
        yield return new WaitForSeconds(3.0f);
        dialog.gameObject.SetActive(true);
        StopCoroutine(tutorialCorutine);
        tutorialCorutine = null;
        
    }

    public void ATKButtonTouched()
    {
        if (!isButtonPress)
        {
            isButtonPress = true;
        }
    }

    public void StartTutorialChapter(int dialogueNum)
    {
        switch (dialogueNum)
        {
            case 2:
                isCorutineStart = false;
                varialbeJoystick.SetActive(true);
                
                currentPlayerPos = player.transform.position;
                break;
            case 3:
                isCorutineStart = false;
                atkButton.SetActive(true);
                break;
            case 4:
                isCorutineStart = false;
                gameManager.SpawnEnemy();
                gameManager.SpawnEnemy();
                break;
        }
        currentChapter = dialogueNum;
    }
}
