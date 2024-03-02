using MyHeroWay;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData", order = 1)]
[System.Serializable]
public class EnemyData : ScriptableObject
{
    public EEnemyType enemyType;
    [Dropdown("GetEnemyNames")]
    public EEnemyName enemyName;
    [ShowAssetPreview] 
    public Sprite enemyIcon;
    [ResizableTextArea]
    public string enemyDescription;
    public SpriteLibraryAsset spriteLibraryAsset;
    [EnumFlags]
    public EElementType elementType;
    [Expandable] 
    public CharacterStatsModifier characterStatsModifier;
    public Color color = Color.red;


    private EEnemyName[] GetEnemyNames()
    {
        EEnemyName[] EnemyNames;
        switch (enemyType)
        {
            case EEnemyType.Slime:
                EnemyNames = new EEnemyName[]
                {
                        EEnemyName.Aquamarine_Slime,
                        EEnemyName.Blue_Slime,
                        EEnemyName.BlueGreen_Slime,
                        EEnemyName.DarkBluee_Slime,
                        EEnemyName.Gold_Slime,
                        EEnemyName.Green_Slime,
                        EEnemyName.Lightblue_Slime,
                        EEnemyName.Maroon_Slime,
                        EEnemyName.Orange_Slime,
                        EEnemyName.Pale_Slime,
                        EEnemyName.Pink_Smile,
                        EEnemyName.Purple_Slime,
                        EEnemyName.Red_Smile

                };
                break;
            case EEnemyType.MiniSmile:
                EnemyNames = new EEnemyName[]
                {
                        EEnemyName.Aquamarine_Baby_Slime,
                        EEnemyName.Blue_Baby_Slime,
                        EEnemyName.BlueGreen_Baby_Slime,
                        EEnemyName.DarkBluee_Baby_Slime,
                        EEnemyName.Gold_Baby_Slime,
                        EEnemyName.Green_Baby_Slime,
                        EEnemyName.Lightblue_Baby_Slime,
                        EEnemyName.Maroon_Baby_Slime,
                        EEnemyName.Orange_Baby_Slime,
                        EEnemyName.Pale_Baby_Slime,
                        EEnemyName.Pink_Baby_Smile,
                        EEnemyName.Purple_Baby_Slime,
                        EEnemyName.Red_Baby_Smile,
                };
                break;
            default:
                EnemyNames = new EEnemyName[]
                {
                        EEnemyName.None
                };
                break;
        }
        return EnemyNames;
    }
}
