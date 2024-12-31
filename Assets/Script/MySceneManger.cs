using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManger : MonoBehaviour
{

    public static MySceneManger Instance {  get; private set; }

    [SerializeField]
    private CanvasGroup fade_IMG;

    float fadeDuration = 2f;

    [SerializeField]
    private GameObject loading;
    [SerializeField]
    private TMP_Text loading_Text;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ChangeScene(string sceneName)
    {
        fade_IMG.DOFade(1, fadeDuration).
            OnStart(() => { 
                fade_IMG.blocksRaycasts = true;
            })
            .OnComplete(() => {
                StartCoroutine(LoadScene(sceneName));            
            });
    }

    IEnumerator LoadScene(string sceneName)
    {
        loading.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        float past_time = 0;
        float percentage = 0;

        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true; //씬 전환 준비 완료
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90) past_time = 0;
            }
            loading_Text.text = percentage.ToString("0") + "%"; //로딩 퍼센트 표기
        }

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        fade_IMG.DOFade(0, fadeDuration)
    .OnStart(() => {
        loading.SetActive(false);
    })
    .OnComplete(() => {
        fade_IMG.blocksRaycasts = false;
    });
    }
}
