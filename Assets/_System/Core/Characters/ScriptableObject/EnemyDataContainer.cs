using MyHeroWay;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataContainer", menuName = "Enemy/EnemyDataContainer", order = 2)]
[System.Serializable]
public class EnemyDataContainer : ScriptableObject
{
    public EnemyData[] enemyDatas;
    public EnemyData GetData(EEnemyName name)
    {
       return enemyDatas.FirstOrDefault(e => e.enemyName == name);
    }
}

