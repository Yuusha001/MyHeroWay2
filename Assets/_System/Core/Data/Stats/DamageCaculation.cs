using UnityEngine;

namespace MyHeroWay.Damage
{
    public class DamageCaculation
    {
        #region Magical Damage formula:
        public static DamageResult GetMagical_DMG(SpellStats spell, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [POW x RANDOM(1~1.125) - MDEF] x [2 + MAG x (Lv+MAG)/256)]  
            bool canKnockBack = isKnockBack(spell, attacker, reciver);
            var damage = (spell.POW * Random.Range(1, 1.125f) - reciver.magicalDefense) * (2 + attacker.magicPower * (attacker.level + attacker.magicPower) / 256);
            if (isCrittical(attacker))
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical(attacker), false, canKnockBack, EDamageType.MAGICAL);
        }

        public static float GetHealAmount(SpellStats spellStats,CharacterStats character)
        {
            //HEAL = POW x RANDOM(1~1.125) x [2 + MAG x (Lv+MAG)/256)]  
            return spellStats.POW* Random.Range(1,1.125f)*(2 + character.magicPower * (character.level + character.magicPower) /256);
        }
        #endregion


        #region Strength formula: swords, greatswords, spears, and crossbows.
        public static DamageResult GetDMG_TypeA(WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            // DMG = [ATK x RANDOM(1~1.125) - DEF] x[1 + STR x(Lv + STR) / 256]
            bool canKnockBack = isKnockBack(weapon, attacker, reciver);
            var damage = (weapon.ATK * Random.Range(1, 1.125f) - reciver.vitality) * (1 + attacker.strength * (attacker.level + attacker.strength) / 256);
            if (isCrittical(attacker))
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical(attacker), false, canKnockBack, EDamageType.PHYSICAL);
        }
        #endregion

        #region Magick Power formula: katana and staves.
        public static DamageResult GetDMG_TypeB(WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(1~1.125) - DEF] x [1 + STR x (Lv+MAG)/256]
            bool canKnockBack = isKnockBack(weapon, attacker, reciver);
            var damage = (weapon.ATK * Random.Range(1, 1.125f) - reciver.vitality) * (1 + attacker.strength * (attacker.level + attacker.magicPower) / 256);
            if (isCrittical(attacker))
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical(attacker), false, canKnockBack, EDamageType.PHYSICAL);
        }

        #endregion

        #region Vitality formula: axes, hammers
        public static DamageResult GetDMG_TypeC(WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(0~1.111) - DEF] x [1 + STR x (Lv+VIT)/128]
            bool canKnockBack = isKnockBack(weapon, attacker, reciver);
            var damage = (weapon.ATK * Random.Range(0, 1.111f) - reciver.vitality) * (1 + attacker.strength * (attacker.level + attacker.vitality) / 128);
            if (isCrittical(attacker))
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical(attacker), false, canKnockBack, EDamageType.PHYSICAL);
        }
        #endregion

        #region Magic Defense formula: Poles 
        public static DamageResult GetDMG_TypeE(WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(1~1.125) - MDEF] x [1 + STR x (Lv+STR)/256]
            bool canKnockBack = isKnockBack(weapon, attacker, reciver);
            var damage = (weapon.ATK * Random.Range(1, 1.125f) - reciver.magicalDefense) * (1 + attacker.strength * (attacker.level + attacker.strength) / 256);
            if (isCrittical(attacker))
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical(attacker), false, canKnockBack, EDamageType.MAGICAL);
        }
        #endregion

        #region Speed formula: daggers, ninja swords, and bows
        public static DamageResult GetDMG_TypeD(WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(1~1.125)]- DEF] x [1 + STR x (Lv+SPD)/218]
            bool canKnockBack = isKnockBack(weapon, attacker, reciver);
            var damage = (weapon.ATK * Random.Range(1, 1.125f) - reciver.vitality) * (1 + attacker.strength * (attacker.level + attacker.speed) / 218);
            if (isCrittical(attacker))
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical(attacker), false, canKnockBack, EDamageType.PHYSICAL);
        }
        #endregion

        #region Mace formula: mace
        public static DamageResult GetDMG_TypeF(WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(1~1.125) - DEF] x [1 + MAG x (Lv+MAG)/256]    
            bool canKnockBack = isKnockBack(weapon, attacker, reciver);
            var damage = (weapon.ATK * Random.Range(1, 1.125f) - reciver.vitality) * (1 + attacker.magicPower * (attacker.level + attacker.magicPower) / 256);
            if (isCrittical(attacker))
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical(attacker), false, canKnockBack, EDamageType.MAGICAL);
        }
        #endregion

        #region Evasion
        //Shield Block = 5% all element, weapon block physical atk
        public static bool isHit(WeaponStats shieldEva, WeaponStats weaponEva, CharacterStats characterEva, bool isMagicalAttack = false)
        {
            int random = Random.Range(0, 100);
            int shieldBlockChance = shieldEva.Evasion * 5;
            bool isShieldBlock = random >= 100 - shieldBlockChance;
            if (isShieldBlock) return true;
            if (isMagicalAttack) return false;
            int totalEva = shieldBlockChance + weaponEva.Evasion;
            float rate = totalEva + (100 - totalEva) * 0.34f;
            bool isWeaponBlock = random >= 100 - rate;
            if (isWeaponBlock) return true;

            bool isCharacterBlock = random >= 100 - characterEva.evasion;
            return isCharacterBlock;
        }
        #endregion

        #region Knock Back
        //Knock Back Chance (%) = Weapon's Knock Back (KB) + RANDOM(0 ~ A's Lv) - RANDOM(0 ~D's Lv) 
        public static bool isKnockBack(WeaponStats weaponStats, CharacterStats attacker, CharacterStats reciver)
        {
            int rate = weaponStats.KnockBackChance + Random.Range(0, attacker.level) - Random.Range(0,reciver.level);
            return Random.Range(0,100) >= 100 - rate;
        }

        public static bool isKnockBack(SpellStats spellStats, CharacterStats attacker, CharacterStats reciver)
        {
            int rate = spellStats.KnockBackChance + Random.Range(0, attacker.level) - Random.Range(0, reciver.level);
            return Random.Range(0, 100) >= 100 - rate;
        }
        #endregion

        #region Success Rate
        //Success Rate = Base Rate + (MAG of user - VIT of target) % 


        #endregion

        #region Crit Hit Rate
        public static bool isCrittical(CharacterStats attacker)
        {
            float rate = (attacker.critRate - 354) / (858 * 5);
            return Random.Range(0, 100) >= 95 - rate;
        }
        #endregion
    }
}