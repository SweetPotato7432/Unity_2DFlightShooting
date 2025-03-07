using UnityEngine;

public class SceneChange : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        MySceneManger.Instance.ChangeScene(sceneName);
        //AudioManager.Instance.ChangeMusic(sceneName);
    }

    public void ExitGame()
    {
        GameSettingData.Instance.InitializeCurrentPlayer();
        Application.Quit();
    }
}
