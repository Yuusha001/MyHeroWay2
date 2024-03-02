using MyHeroWay;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDictionary", menuName = "Enemy/EnemyDictionary", order = 3)]
[System.Serializable]
public class EnemyDictionary : ScriptableObject
{
    public EnemyType[] data;

    public EnemyDataContainer GetEnemyData(EEnemyType enemyType)
    {
       return data.FirstOrDefault(e => e.enemyType == enemyType).enemyDataContainer;
    }
}

[System.Serializable]
public class EnemyType
{
    public EEnemyType enemyType;
    public EnemyDataContainer enemyDataContainer;
}
