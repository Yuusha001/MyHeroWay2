using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyHeroWay;
using NaughtyAttributes;

public class DataManager : Singleton<DataManager>
{
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
    public DamageNumbersPro.DamageNumber healTextEff;
}
