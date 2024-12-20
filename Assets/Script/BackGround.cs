using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    private GameObject[] topBg;
    [SerializeField]
    private GameObject[] middleBg;
    [SerializeField]
    private GameObject[] bottomBg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBG(topBg, 5f);
        MoveBG(middleBg, 3f);
        MoveBG(bottomBg, 1f);
    }

    void MoveBG(GameObject[] bg, float scrollSpeed)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = bg[i].transform.position;
            pos.y = pos.y - scrollSpeed * Time.deltaTime;
            bg[i].transform.position = pos;

            if (bg[i].transform.position.y < -20)
            {
                Vector3 pos2 = bg[i].transform.position;
                pos2.y = pos.y + 36f;
                bg[i].transform.position = pos2;
            }
        }
    }
}
