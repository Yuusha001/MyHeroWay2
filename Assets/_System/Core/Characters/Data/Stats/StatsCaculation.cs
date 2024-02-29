using UnityEngine;

namespace MyHeroWay.Stats
{
    public class StatsCaculation 
    {
        static float GetFinalHP(int level, CharacterStatsModifier characterStatsModifier)
        {
            var characterStatsBonus = DataManager.Instance.characterStatsBonus;
            int sumBonus = 0;
            for (int i = 0; i <= level; i++)
            {
                var range = characterStatsBonus.statsEntries[i].hpBonus;
                sumBonus += Random.Range(range.x, range.y);
            }
            sumBonus += characterStatsModifier.baseHP;
            return sumBonus*characterStatsModifier.modHP;
        }

        static float GetFinalMP(int level, CharacterStatsModifier characterStatsModifier)
        {
            var characterStatsBonus = DataManager.Instance.characterStatsBonus;
            int sumBonus = 0;
            for (int i = 0; i <= level; i++)
            {
                var range = characterStatsBonus.statsEntries[i].mpBonus;
                sumBonus += Random.Range(range.x, range.y);
            }
            sumBonus += characterStatsModifier.baseMP;
            return sumBonus * characterStatsModifier.modMP;
        }

        static float GetFinalSpeed(int level, CharacterStatsModifier characterStatsModifier)
        {
            return characterStatsModifier.baseSPD + (level * characterStatsModifier.modSPD) / 128;
        }
        static float GetFinalVitality(int level, CharacterStatsModifier characterStatsModifier)
        {
            return characterStatsModifier.baseVIT + (level * characterStatsModifier.modVIT) / 128;
        }

        static float GetFinalStrength(int level, CharacterStatsModifier characterStatsModifier)
        {
            return characterStatsModifier.baseSTR + (level * characterStatsModifier.modSTR) / 128;
        }

        static float GetFinalMagicPower(int level, CharacterStatsModifier characterStatsModifier)
        {
            return characterStatsModifier.baseMAG + (level * characterStatsModifier.modMAG) / 128;
        }

        static float  GetFinalMagicDefense(int level, CharacterStatsModifier characterStatsModifier)
        {
            return characterStatsModifier.baseMDEF + (level * characterStatsModifier.modMDEF) / 128;
        }

        public static int GetNextLevelExp(int level)
        {
            return Mathf.RoundToInt(0.1f * Mathf.Pow(level, 4) + 4.2f * Mathf.Pow(level, 3) + 6.1f * Mathf.Pow(level, 2) + 1.4f * level - 11.4f);
        }

        public static CharacterStats GetFinalCharacterStats(int level, CharacterStatsModifier characterStatsModifier)
        {
            CharacterStats characterStats = new CharacterStats();
            characterStats.level = level;
            characterStats.health = GetFinalHP(level, characterStatsModifier);
            characterStats.mana = GetFinalMP(level, characterStatsModifier);
            characterStats.vitality = GetFinalVitality(level, characterStatsModifier);
            characterStats.magicalDefense = GetFinalMagicDefense(level, characterStatsModifier);
            characterStats.magicPower = GetFinalMagicPower(level, characterStatsModifier);
            characterStats.strength = GetFinalStrength(level, characterStatsModifier);
            characterStats.speed = GetFinalSpeed(level, characterStatsModifier);
            return characterStats;
        }

        public static float GetRealSpeedByStats(float speed)
        {
            if (speed >= 94) return 3.75f;
            if (speed >= 73) return 3.65f;
            if (speed >= 57) return 3.55f;
            if (speed >= 47) return 3.45f;
            if (speed >= 41) return 3.35f;
            if (speed >= 36) return 3.25f;
            if (speed >= 32) return 3.15f;
            if (speed >= 29) return 3.05f;
            if (speed >= 26) return 2.95f;
            if (speed >= 24) return 2.85f;
            if (speed >= 22) return 2.75f;
            if (speed >= 20) return 2.65f;
            if (speed >= 18) return 2.55f;
            if (speed >= 14) return 2.45f;
            if (speed >= 12) return 2.35f;
            return 2.25f;
        }
    }
}
