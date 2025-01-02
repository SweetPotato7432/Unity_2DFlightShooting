using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetAllData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveData(string key, object value)
    {
        if (value is int)
        {
            PlayerPrefs.SetInt(key, (int)value);
        }
        else if (value is float)
        {
            PlayerPrefs.SetFloat(key, (float)value);
        }
        else if (value is string)
        {
            PlayerPrefs.SetString(key, (string)value);
        }
        else
        {
            Debug.LogError("�������� �ʴ� ������ Ÿ���Դϴ�.");
            return;
        }

        PlayerPrefs.Save();
        Debug.Log($"Data Saved: {key} = {value}");
    }

    public int LoadData(string key,int defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }
    public float LoadData(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }
    public string LoadData(string key, string defaultValue)
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    // Ư�� Ű�� �ش��ϴ� �����͸� �����ϴ� �Լ�
    public void DeleteData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
            Debug.Log($"Data deleted : {key}");
        }
        else
        {
            Debug.Log($"No data found for Key : {key}");
        }
    }

    // ��� �����͸� �����ϴ� �Լ�
    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log($"All data deleted");
    }

}
