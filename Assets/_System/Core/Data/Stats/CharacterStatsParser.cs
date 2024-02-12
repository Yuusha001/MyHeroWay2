using MyHeroWay;
using NaughtyAttributes;
using System;
using UnityEngine;

public class ExcelDataParser : MonoBehaviour
{
    public TextAsset excelData;
    public CharacterStatsBonus test;
    [Button("ParseExcelData")]
    public void Test()
    {
        test = ParseExcelData();
       
    }



    public CharacterStatsBonus ParseExcelData()
    {
        CharacterStatsBonus statsData = ScriptableObject.CreateInstance<CharacterStatsBonus>();

        string[] data = excelData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 3 - 1;

        for (int i = 0; i < tableSize; i++) // Start from index 1 to skip header row
        {
            CharacterStatsBonus.StatsBonus entry = new CharacterStatsBonus.StatsBonus();

            entry.level = int.Parse(data[3 * (i + 1)]);
            entry.hpBonus = ParseRange(data[3 * (i + 1) + 1]);
            entry.mpBonus = ParseRange(data[3 * (i + 1) + 2]);

            statsData.statsEntries.Add(entry);
        }
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        string assetPath = $"Assets/Resources/_Scriptable Objects/Stats Bonus/test.asset";
        UnityEditor.AssetDatabase.CreateAsset(statsData, assetPath);
        return statsData;
    }

    private Vector2Int ParseRange(string range)
    {
        string[] rangeParts = range.Split('-');
        int min = int.Parse(rangeParts[0].Trim());
        int max = int.Parse(rangeParts[1].Trim());
        return new Vector2Int(min, max);
    }
}