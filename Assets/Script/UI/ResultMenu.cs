using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ResultMenu : MonoBehaviour
{
    [SerializeField]
    GameObject ResultText;
    [SerializeField]
    GameObject ScoreText;
    [SerializeField]
    GameObject RetryButton;
    [SerializeField]
    GameObject LeaderBoardButton;
    [SerializeField]
    GameObject ExitButton;

    private void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        ResultText.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 340), 2.0f);
        yield return new WaitForSeconds(1.5f);
        ScoreText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        RetryButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -260), 1.0f);
        yield return new WaitForSeconds(0.5f);
        LeaderBoardButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -380), 1.0f);
        yield return new WaitForSeconds(0.5f);
        ExitButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -500), 1.0f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
