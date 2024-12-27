using UnityEngine;

public class Callback_Lamda : MonoBehaviour
{
    // �ݹ� �Լ�
    // 1. ���� ȣ������ �ʴ´�.
    // 2. ȣ���� �޶�� �Լ� �̸��� �Ѱ��ش�.
    // ex) ��ư OnClick -> �Լ�
    // 
    // ���� �ڵ�(�ݹ��Լ� ���)
    // 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PerformCalculation(
            10,
            20,
            // OnPlayCallback
            // ���� �ڵ�
            (res,res2) =>
            {
                return 15f;
            }
            );
    }

    // Action : �Լ��� �Ű������� �ѱ��.
    void PerformCalculation(int a, int b, System.Action callback)
    {
        int result = a + b;
        callback?.Invoke();// �Ű� ������ �Ѿ�� �ݹ� �Լ��� ȣ��
    }

    // �ݹ� �Լ�
    void OnPlayCallback()
    {
        Debug.Log("OnPlayCallback ȣ��");
    }

    // ��ȯ���� �Ű� ������ �ִ� �ݹ��Լ� ȣ��
    void PerformCalculation(int a, int b, System.Func<int,int,float> callback)
    {
        int result = a + b;
        float c = callback?.Invoke(7,8) ?? 0f; // �Ű� ������ �Ѿ�� �ݹ� �Լ��� ȣ��
        Debug.Log(c);
    }

    float OnPlayCallback(int a, int b)
    {
        Debug.Log("OnPlayCallback ȣ��");
        return a + b;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
