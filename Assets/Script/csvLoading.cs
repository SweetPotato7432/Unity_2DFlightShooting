using UnityEngine;
using System.Collections.Generic;

public class CSVLoading
{
    //csv�� ����Ʈ ����
    // string ����Ʈ�� ����Ʈ��
    private List<List<string>> csvData = new List<List<string>>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        //// TextAsset : 
        //// csv������ �ҷ����� ������
        //TextAsset csvFile = Resources.Load<TextAsset>("Example");
        //if(csvFile != null)
        //{
        //    Debug.Log("������ �����մϴ�.");
        //    // '\n' : �ٹٲ�
        //    // �ٹٲ��� ã�Ƽ� �и��Ѵ�.
        //    string[] rows = csvFile.text.Split('\n');

        //    foreach (string row in rows)
        //    {
        //        string[] fields = row.Split(',');
        //        List<string> rowData = new List<string>(fields);
        //        csvData.Add(rowData);
        //    }

        //    int row_num = 0;
        //    foreach(List<string> row in csvData)
        //    {
        //        Debug.Log("["+(row_num+1)+"]��");
        //        int field_num = 0;
        //        foreach(string field in row)
        //        {
        //            switch (field_num)
        //            {
        //                case 0:
        //                    Debug.Log($"1�� : {int.Parse(field)}");
        //                    break;
        //                case 1:
        //                    Debug.Log($"2�� : {int.Parse(field)}");
        //                    break;
        //                case 2:
        //                    Debug.Log($"3�� : {float.Parse(field)}");
        //                    break;
        //                case 3:
        //                    Debug.Log($"4�� : {float.Parse(field)}");
        //                    break;
        //            }
        //            field_num++;
        //        }
                
        //    }
            
        //}
        //else
        //{
        //    Debug.LogError("������ �������� �ʽ��ϴ�.");
        //}


    }

    public List<List<string>> csvLoad(string csvName)
    {
        // TextAsset : 
        // csv������ �ҷ����� ������
        TextAsset csvFile = Resources.Load<TextAsset>(csvName);
        if (csvFile != null)
        {
            Debug.Log("������ �����մϴ�.");
            // '\n' : �ٹٲ�
            // �ٹٲ��� ã�Ƽ� �и��Ѵ�.
            string[] rows = csvFile.text.Split('\n');

            foreach (string row in rows)
            {
                string[] fields = row.Split(',');
                List<string> rowData = new List<string>(fields);
                csvData.Add(rowData);
            }
        }
        else
        {
            Debug.LogError("������ �������� �ʽ��ϴ�.");
        }

        return csvData;
    }



}
