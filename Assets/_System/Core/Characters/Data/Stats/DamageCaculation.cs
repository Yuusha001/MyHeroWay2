using Random = UnityEngine.Random;

namespace MyHeroWay.Damage
{
    //Random must call in Main Thread
    public class DamageCaculation
    {
        #region Magical Damage formula:
        static DamageResult GetMagical_DMG(bool isCrittical,float random ,SpellStats spell, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [POW x RANDOM(1~1.125) - MDEF] x [2 + MAG x (Lv+MAG)/256)]  
            var damage = (spell.POW * random - reciver.magicalDefense) * (2 + attacker.magicPower * (attacker.level + attacker.magicPower) / 256);
            if (isCrittical)
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical, false, EDamageType.MAGICAL);
        }

        public static float GetHealAmount(SpellStats spellStats,CharacterStats character)
        {
            //HEAL = POW x RANDOM(1~1.125) x [2 + MAG x (Lv+MAG)/256)]  
            return spellStats.POW* UnityEngine.Random.Range(1,1.125f)*(2 + character.magicPower * (character.level + character.magicPower) /256);
        }
        #endregion


        #region Strength formula: swords, greatswords, spears, and crossbows.
        static DamageResult GetDMG_TypeA(bool isCrittical, float random, WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            // DMG = [ATK x RANDOM(1~1.125) - DEF] x[1 + STR x(Lv + STR) / 256]
            var damage = (weapon.ATK * random - reciver.vitality) * (1 + attacker.strength * (attacker.level + attacker.strength) / 256);
            if (isCrittical)
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical, false, EDamageType.PHYSICAL);
        }
        #endregion

        #region Magick Power formula: katana and staves.
        static DamageResult GetDMG_TypeB(bool isCrittical, float random, WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(1~1.125) - DEF] x [1 + STR x (Lv+MAG)/256]
            var damage = (weapon.ATK * random - reciver.vitality) * (1 + attacker.strength * (attacker.level + attacker.magicPower) / 256);
            if (isCrittical)
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical, false, EDamageType.PHYSICAL);
        }

        #endregion

        #region Vitality formula: axes, hammers
        static DamageResult GetDMG_TypeC(bool isCrittical, float random, WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(0~1.111) - DEF] x [1 + STR x (Lv+VIT)/128]
            var damage = (weapon.ATK * random - reciver.vitality) * (1 + attacker.strength * (attacker.level + attacker.vitality) / 128);
            if (isCrittical)
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical, false, EDamageType.PHYSICAL);
        }
        #endregion

        #region Magic Defense formula: Poles 
        static DamageResult GetDMG_TypeE(bool isCrittical, float random, WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(1~1.125) - MDEF] x [1 + STR x (Lv+STR)/256]
            var damage = (weapon.ATK * random - reciver.magicalDefense) * (1 + attacker.strength * (attacker.level + attacker.strength) / 256);
            if (isCrittical)
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical, false, EDamageType.MAGICAL);
        }
        #endregion

        #region Speed formula: daggers, ninja swords, and bows
        static DamageResult GetDMG_TypeD(bool isCrittical, float random, WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(1~1.125)]- DEF] x [1 + STR x (Lv+SPD)/218]
            var damage = (weapon.ATK * random - reciver.vitality) * (1 + attacker.strength * (attacker.level + attacker.speed) / 218);
            if (isCrittical)
            {   
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical, false, EDamageType.PHYSICAL);
        }
        #endregion

        #region Mace formula: mace
        static DamageResult GetDMG_TypeF(bool isCrittical, float random, WeaponStats weapon, CharacterStats attacker, CharacterStats reciver)
        {
            //DMG = [ATK x RANDOM(1~1.125) - DEF] x [1 + MAG x (Lv+MAG)/256]    
            var damage = (weapon.ATK * random - reciver.vitality) * (1 + attacker.magicPower * (attacker.level + attacker.magicPower) / 256);
            if (isCrittical)
            {
                damage *= 1.4483610f;
            }
            return new DamageResult(damage, isCrittical, false, EDamageType.MAGICAL);
        }
        #endregion

        #region Evasion
        //Shield Block = 5% all element, weapon block physical atk
        public static bool IsHit(int weaponEva, CharacterStats characterEva, int shieldEva = 0, bool isMagicalAttack = false)
        {
            int random = Random.Range(0, 100);
            int shieldBlockChance = shieldEva * 5;
            bool isShieldBlock = random >= 100 - shieldBlockChance;
            if (isShieldBlock) return true;
            if (isMagicalAttack) return false;
            int totalEva = shieldBlockChance + weaponEva;
            float rate = totalEva + (100 - totalEva) * 0.34f;
            bool isWeaponBlock = random >= 100 - rate;
            if (isWeaponBlock) return true;

            bool isCharacterBlock = random >= 100 - characterEva.evasion;
            return isCharacterBlock;
        }
        #endregion

        #region Knock Back
        //Knock Back Chance (%) = Weapon's Knock Back (KB) + RANDOM(0 ~ A's Lv) - RANDOM(0 ~D's Lv) 
        // Sword, Axe, Hammer, Spear, Pole, Staff = 10
        public static bool IsKnockBack(DamageInfo damageInfo, CharacterStats reciver)
        {
            WeaponStats weaponStats = damageInfo.fromPrimary ? damageInfo.primaryWeaponStats : damageInfo.secondaryWeaponStats;
            CharacterStats attacker = damageInfo.owner.runtimeStats;
            int rate = weaponStats.KnockBackChance + Random.Range(0, attacker.level) - Random.Range(0,reciver.level);
            return Random.Range(0,100) >= 100 - rate;
        }

        public static bool IsKnockBack(SpellStats spellStats, CharacterStats attacker, CharacterStats reciver)
        {
            int rate = spellStats.KnockBackChance + Random.Range(0, attacker.level) - Random.Range(0, reciver.level);
            return Random.Range(0, 100) >= 100 - rate;
        }
        #endregion

        #region Success Rate
        //Success Rate = Base Rate + (MAG of user - VIT of target) % 


        #endregion

        #region Crit Hit Rate
        public static bool IsCrittical(CharacterStats attacker)
        {
            float rate = (attacker.critRate - 354) / (858 * 5);
            return Random.Range(0, 100) >= 95 - rate;
        }
        #endregion

        public static float RandomByWeaponType(EWeaponType weaponType)
        {
            switch (weaponType)
            {
                case EWeaponType.Sword:
                    return Random.Range(1, 1.125f);
                case EWeaponType.GreatSword:
                    return Random.Range(1, 1.125f);
                case EWeaponType.Spear:
                    return Random.Range(1, 1.125f);
                case EWeaponType.Bow:
                    return Random.Range(1, 1.125f);
                case EWeaponType.CrossBow:
                    return Random.Range(1, 1.125f);
                case EWeaponType.Katana:
                    return Random.Range(1, 1.125f);
                case EWeaponType.Staves:
                    return Random.Range(1, 1.125f);
                case EWeaponType.Axe:
                    return Random.Range(0, 1.111f);
                case EWeaponType.Hammer:
                    return Random.Range(0, 1.111f);
                case EWeaponType.Pole:
                    return  Random.Range(1, 1.125f);
                case EWeaponType.Dagger:
                    return Random.Range(1, 1.125f);
                case EWeaponType.Mace:
                    return Random.Range(1, 1.125f);
            }
            return Random.Range(1, 1.125f);

        }


        public static DamageResult GetDamageResult(DamageInfo damageInfo, CharacterStats reciver, bool isCrit, float random)
        {
            WeaponStats weapon = damageInfo.fromPrimary ? damageInfo.primaryWeaponStats : damageInfo.secondaryWeaponStats;
            CharacterStats attacker = damageInfo.owner.runtimeStats;
            
            switch (damageInfo.weaponType)
            {
                //type A
                case EWeaponType.Sword:
                    return GetDMG_TypeA(isCrit, random ,weapon, attacker, reciver);
                case EWeaponType.GreatSword:
                    return GetDMG_TypeA(isCrit, random, weapon, attacker, reciver);
                case EWeaponType.Spear:
                    return GetDMG_TypeA(isCrit, random, weapon, attacker, reciver);
                case EWeaponType.CrossBow:
                    return GetDMG_TypeA(isCrit, random, weapon, attacker, reciver);

                //Type B
                case EWeaponType.Katana:
                    return GetDMG_TypeB(isCrit, random, weapon, attacker, reciver);
                case EWeaponType.Staves:
                    return GetDMG_TypeB(isCrit, random, weapon, attacker, reciver);
                
                //Type C
                case EWeaponType.Axe:
                    return GetDMG_TypeC(isCrit, random, weapon, attacker, reciver);
                case EWeaponType.Hammer:
                    return GetDMG_TypeC(isCrit, random, weapon, attacker, reciver);
                

                //Type D
                case EWeaponType.Bow:
                    return GetDMG_TypeD(isCrit, random, weapon, attacker, reciver);
                case EWeaponType.Dagger:
                    return GetDMG_TypeD(isCrit, random, weapon, attacker, reciver);
                //Type F
                case EWeaponType.Mace:
                    return GetDMG_TypeF(isCrit, random, weapon, attacker, reciver);
                
                //Type E
                case EWeaponType.Pole:
                    return GetDMG_TypeE(isCrit, random, weapon, attacker, reciver);
            }
            return new DamageResult();
        }

        public static int TotalEva(DamageInfo damageInfo)
        {
            int total = damageInfo.primaryWeaponStats.Evasion;
            if (damageInfo.secondaryWeaponStats == null)
                return total;
            return total + damageInfo.secondaryWeaponStats.Evasion;
        }
    }
}