using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

public struct CharacterData
{
    public int Key;
    public int Level;
    public float AttackPower;
    public float MaxHp;
    public float MoveSpeed;
    public float AttackSpeed;
}

public class DataManager
{
    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }

            return _instance;
        }
    }

    private Dictionary<int, CharacterData> _characterDatas = new Dictionary<int, CharacterData>();
    public Dictionary<int, CharacterData> CharacterDatas
    {
        get { return _characterDatas; }
    }
    public CharacterData GetCharacterData(int key) { return _characterDatas[key]; }

    public void LoadCharacterData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/CharacterStatusTable");

        string text = textAsset.text;

        string[] rowData = text.Split("\r\n");

        for (int i = 1; i < rowData.Length; i++)
        {
            if (rowData[i].Length == 0)
                break;

            string[] datas = rowData[i].Split(",");

            CharacterData data;
            data.Key = int.Parse(datas[0]);
            data.Level = int.Parse(datas[1]);
            data.AttackPower = float.Parse(datas[2]);
            data.MaxHp = float.Parse(datas[3]);
            data.MoveSpeed = float.Parse(datas[4]);
            data.AttackSpeed = float.Parse(datas[5]);

            _characterDatas.Add(data.Key, data);
        }
    }
}