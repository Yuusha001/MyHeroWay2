using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyHeroWay;
using NaughtyAttributes;
using System;

public class DataManager : Singleton<DataManager>
{
    [ReadOnly]
    public UserData data;
    public static event Action OnLevelUp;
    public static event Action OnLevelEXP;
    [ReadOnly]
    public CharacterStatsBonus characterStatsBonus;
    [ReadOnly]
    public EnemyDictionary enemyDictionary;
    [ReadOnly]
    public ElementDictionary elementDictionary;
    [ReadOnly]
    public ElementDictionary elementWeaponDictionary;
    [Foldout("Text Effect")]
    public DamageNumbersPro.DamageNumber damageTextEff;
    [Foldout("Text Effect")]
    public DamageNumbersPro.DamageNumber expTextEff;
    [Foldout("Text Effect")]
    public DamageNumbersPro.DamageNumber healTextEff;
    private string userPrefData
    {
        get { return PlayerPrefs.GetString("sd_data", ""); }
        set { PlayerPrefs.SetString("sd_data", value); }
    }

    public void LoadData()
    {
        string pref = userPrefData;
        if (!string.IsNullOrEmpty(pref))
        {
            data = JsonUtility.FromJson<UserData>(userPrefData);
        }
        else
        {
            data = new UserData();
        }
        Debug.Log("Load:" + pref);
    }
    public void SaveData()
    {
        string dataJson = JsonUtility.ToJson(data);
        userPrefData = dataJson;
        Debug.Log("Save:" + dataJson);
    }

    public void LevelUp()
    {
        SaveData();
        OnLevelUp?.Invoke();
    }

    public void AddExp(CharacterExp exp)
    {
        data.userEXP = exp.CurrentEXP;
        data.userLevel = exp.CurrentLevel;
        SaveData();
        OnLevelEXP?.Invoke();
    }
}
