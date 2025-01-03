using UnityEngine;

public class ResetRank : MonoBehaviour
{
    
    public void ResetRankButton()
    {
        GameSettingData.Instance.ResetRanking();
    }
}
