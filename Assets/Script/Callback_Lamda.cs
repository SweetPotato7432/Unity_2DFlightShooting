using UnityEngine;

public class Callback_Lamda : MonoBehaviour
{
    // 콜백 함수
    // 1. 내가 호출하지 않는다.
    // 2. 호출해 달라고 함수 이름을 넘겨준다.
    // ex) 버튼 OnClick -> 함수
    // 
    // 람다 코드(콜백함수 축약)
    // 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PerformCalculation(
            10,
            20,
            // OnPlayCallback
            // 람다 코드
            (res,res2) =>
            {
                return 15f;
            }
            );
    }

    // Action : 함수를 매개변수로 넘긴다.
    void PerformCalculation(int a, int b, System.Action callback)
    {
        int result = a + b;
        callback?.Invoke();// 매개 변수로 넘어온 콜백 함수를 호출
    }

    // 콜백 함수
    void OnPlayCallback()
    {
        Debug.Log("OnPlayCallback 호출");
    }

    // 반환값과 매개 변수가 있는 콜백함수 호출
    void PerformCalculation(int a, int b, System.Func<int,int,float> callback)
    {
        int result = a + b;
        float c = callback?.Invoke(7,8) ?? 0f; // 매개 변수로 넘어온 콜백 함수를 호출
        Debug.Log(c);
    }

    float OnPlayCallback(int a, int b)
    {
        Debug.Log("OnPlayCallback 호출");
        return a + b;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
