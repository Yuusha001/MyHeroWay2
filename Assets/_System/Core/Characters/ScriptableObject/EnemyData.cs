using MyHeroWay;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData", order = 1)]
[System.Serializable]
public class EnemyData : ScriptableObject
{
    public EEnemyName enemyName;
    public Sprite enemyIcon;
    [ResizableTextArea]
    public string enemyDescription;
    public SpriteLibraryAsset spriteLibraryAsset;
    public EElementType elementType;
    public CharacterStatsModifier characterStatsModifier;
}
