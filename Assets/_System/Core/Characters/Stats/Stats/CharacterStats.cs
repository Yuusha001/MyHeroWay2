namespace MyHeroWay
{
    [System.Serializable]
    public class CharacterStats 
    {
        public int level;
        public float health;
        public float mana;
        public float strength;
        public float magicPower;
        public float speed;
        public float vitality;
        public float magicalDefense;
        public float critRate;
        public float critDamage;
        public float evasion;

        public static CharacterStats operator +(CharacterStats a, CharacterStats b)
        {
            CharacterStats newStats = new CharacterStats();
            newStats.health = a.health + b.health;
            newStats.mana = a.mana + b.mana;
            newStats.vitality = a.vitality + b.vitality;
            newStats.critRate = a.critRate + b.critRate;
            newStats.critDamage = a.critDamage + b.critDamage;
            newStats.evasion = a.evasion + b.evasion;
            newStats.speed = a.speed + b.speed;
            newStats.strength= a.strength+ b.strength;
            newStats.magicPower = a.magicPower + b.magicPower;
            newStats.magicalDefense = a.magicalDefense + b.magicalDefense;
            return newStats;
        }

        public CharacterStats(CharacterStats reference)
        {
            this.level = reference.level;
            this.health = reference.health;
            this.mana = reference.mana;
            this.vitality = reference.vitality;
            this.strength= reference.strength;
            this.magicPower = reference.magicPower;
            this.magicalDefense = reference.magicalDefense;
            this.critRate = reference.critRate;
            this.critDamage = reference.critDamage;
            this.evasion = reference.evasion;
            this.speed = reference.speed;
        }

        public CharacterStats()
        {
            this.level = 0;
            this.health = 0;
            this.mana = 0;
            this.vitality  = 0;
            this.strength = 0;
            this.magicPower = 0;
            this.magicalDefense = 0;
            this.evasion = 0;
            this.speed = 0;
            this.critRate = 0;
            this.critDamage = 0;
        }
    }
}
