using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public void LoadBattleScene()
    {
        MySceneManger.Instance.ChangeScene("BattleScene");
    }
}
