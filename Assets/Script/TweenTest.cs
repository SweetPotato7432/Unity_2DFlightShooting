using DG.Tweening;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //209,507
        GetComponent<RectTransform>().DOAnchorPos(new Vector2(209, 507), 0.7f);
        GetComponent<RectTransform>().DOScale(3.0f, 0.7f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
