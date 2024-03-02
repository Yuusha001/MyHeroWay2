using NaughtyAttributes;
using UnityEngine;

namespace MyHeroWay
{
    [System.Serializable]
    public class CharacterExp
    {
        [Foldout("Exp")]
        public int CurrentLevel;
        [Foldout("Exp")]
        public int CurrentEXP;
        [Foldout("Exp")]
        public int NextLevelEXP;

        public CharacterExp(int currentLevel, int currentEXP)
        {
            CurrentLevel = currentLevel;
            CurrentEXP = currentEXP;
            int temp = currentLevel + 1;
            NextLevelEXP = Stats.StatsCaculation.GetNextLevelExp(temp);
            Debug.Log("Next level exp " + NextLevelEXP);
        }

        public CharacterExp()
        {
            CurrentLevel = 1;
            CurrentEXP = 0;
            NextLevelEXP = Stats.StatsCaculation.GetNextLevelExp(2);
        }

        public void AddEXP(int value, System.Action callback = null)
        {
            CurrentEXP += value;
            int diff = NextLevelEXP - CurrentEXP;
            if (diff <= 0)
            {
                LevelUP(-1 * diff);
                callback();
            }
        }

        public void LevelUP(int diff)
        {
            CurrentLevel++;
            CurrentEXP = diff;
            NextLevelEXP = Stats.StatsCaculation.GetNextLevelExp(CurrentLevel);
        }

        public float GetPercentComplete()
        {
            return System.MathF.Round(GetNomalize() * 100,2);
        }

        public float GetNomalize()
        {
            return CurrentEXP*1.00f / NextLevelEXP;
        }
    }
}
