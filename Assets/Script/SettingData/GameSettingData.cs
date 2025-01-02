using UnityEngine;
using UnityEngine.Audio;

public class GameSettingData : MonoBehaviour
{
    private int[] bestScore = new int[10];

    public static GameSettingData Instance { get; private set; }
    [SerializeField]
    private AudioMixer audioMixer;

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioSettings(); // PlayerPrefs���� ������ �ҷ�����
            InitializeCurrentPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
    }

    // ���� ���� �� �������� ����
    public void SaveScore(int currentScore)
    {
        bool firstSwap = true;

        int tempScore = 0;

        for (int i = 0; i < 10; i++)
        {
            bestScore[i] = PlayerPrefs.GetInt(i + "BestScore");

            while (bestScore[i] < currentScore)
            {
                if (firstSwap)
                {
                    PlayerPrefs.SetInt("CurrentPlayerRank", i);
                    firstSwap = false;
                }

                tempScore = bestScore[i];
                bestScore[i] = currentScore;
                PlayerPrefs.SetInt(i+"BestScore", currentScore);

                currentScore = tempScore;
            }
            if (firstSwap)
            {
                InitializeCurrentPlayer();
            }
        }
    }

    public void InitializeCurrentPlayer()
    {
        PlayerPrefs.SetInt("CurrentPlayerRank", -1);
    }

    // ����� ����
    public void SaveAudioSettings(float bgmVolume, float sfxVolume)
    {
        PlayerPrefs.SetFloat("BGM", bgmVolume);
        PlayerPrefs.SetFloat("SFX", sfxVolume);
        PlayerPrefs.Save();
    }

    private void LoadAudioSettings()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BGM", 1f); // �⺻�� 0.75
        float sfxVolume = PlayerPrefs.GetFloat("SFX", 1f); // �⺻�� 0.75

        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);

        //bool isBGMMuted = PlayerPrefs.GetInt("BGMMute", 0) == 1;
        //bool isSFXMuted = PlayerPrefs.GetInt("SFXMute", 0) == 1;

        //SetBGMMute(isBGMMuted);
        //SetSFXMute(isSFXMuted);
    }

    // BGM ���� ���� (AudioMixer�� BGM �׷� ���� ����)
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
        SaveAudioSettings(volume, PlayerPrefs.GetFloat("SFX", 0.75f));
    }

    // SFX ���� ���� (AudioMixer�� SFX �׷� ���� ����)
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
        SaveAudioSettings(PlayerPrefs.GetFloat("BGM", 0.75f), volume);
    }

    //public void SetBGMMute(bool isMuted)
    //{
    //    if (isMuted)
    //    {
    //        audioMixer.SetFloat("BGM", -80);
    //    }
    //    else
    //    {
    //        float volume = PlayerPrefs.GetFloat("BGM", 0.75f);
    //        SetBGMVolume(volume);
    //    }
    //    PlayerPrefs.SetInt("BGMMute", isMuted ? 1 : 0);// ���Ұ� ���� ����
    //}

    //public void SetSFXMute(bool isMuted)
    //{
    //    if (isMuted)
    //    {
    //        audioMixer.SetFloat("SFX", -80);
    //    }
    //    else
    //    {
    //        float volume = PlayerPrefs.GetFloat("SFX", 0.75f);
    //        SetSFXVolume(volume);
    //    }
    //    PlayerPrefs.SetInt("SFXMute", isMuted ? 1 : 0);// ���Ұ� ���� ����
    //}
}
