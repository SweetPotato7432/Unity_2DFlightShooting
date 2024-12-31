using UnityEngine;
using System.Collections.Generic;

public class EffectPoolManager : MonoBehaviour
{
    public static EffectPoolManager Instance { get; private set; }

    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private int initialPoolSize = 10;

    private Queue<Effect> effectPool = new Queue<Effect> ();

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
        InitializePool();
    }

    // 풀 초기화
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewEffect();
        }

    }

    // 적 생성
    private void CreateNewEffect()
    {
        GameObject effectObject = Instantiate(effectPrefab);
        Effect effect = effectObject.GetComponent<Effect>();
        effect.Deactive();
    }

    // 적 사용
    public Effect GetEffect( Vector3 position, Effect.AnimType animType)
    {
        if (effectPool.Count == 0)
        {
            CreateNewEffect();
        }

        Effect effect = effectPool.Dequeue();
        effect.Initalize(position, animType);

        return effect;
    }

    public void ReturnEffect(Effect effect)
    {
        effectPool.Enqueue(effect);
    }
}
