namespace MyHeroWay
{
    public class CharacterStats 
    {
        public int health;
        public int mana;
        public int physicalDamage;
        public int magicalDamage;
        public int armor;
        public int critRate;
        public int critDamage;
        public int dodgeChance;
        public int coolDown;
        public int crowdControl;
        public int hpRegeneration;
        public int mpRegeneration;

        public static CharacterStats operator +(CharacterStats a, CharacterStats b)
        {
            CharacterStats newStats = new CharacterStats();
            newStats.health = a.health + b.health;
            newStats.mana = a.mana + b.mana;
            newStats.armor = a.armor + b.armor;
            newStats.critRate = a.critRate + b.critRate;
            newStats.critDamage = a.critDamage + b.critDamage;
            newStats.coolDown = a.coolDown + b.coolDown;
            newStats.dodgeChance = a.dodgeChance + b.dodgeChance;
            newStats.crowdControl = a.crowdControl + b.crowdControl;
            newStats.physicalDamage = a.physicalDamage + b.physicalDamage;
            newStats.magicalDamage = a.magicalDamage + b.magicalDamage;
            newStats.hpRegeneration = a.hpRegeneration + b.hpRegeneration;
            newStats.mpRegeneration = a.mpRegeneration + b.mpRegeneration;
            return newStats;
        }

        public CharacterStats(CharacterStats reference)
        {
            this.health = reference.health;
            this.mana = reference.mana;
            this.armor = reference.armor;
            this.physicalDamage = reference.physicalDamage;
            this.magicalDamage = reference.magicalDamage;
            this.critRate = reference.critRate;
            this.critDamage = reference.critDamage;
            this.coolDown = reference.coolDown;
            this.dodgeChance = reference.dodgeChance;
            this.crowdControl = reference.crowdControl;
            this.hpRegeneration = reference.hpRegeneration;
            this.mpRegeneration = reference.mpRegeneration;
        }

        public CharacterStats()
        {
            this.health = 0;
            this.mana = 0;
            this.armor = 0;
            this.hpRegeneration = 0;
            this.mpRegeneration = 0;
            this.physicalDamage = 0;
            this.magicalDamage = 0;
            this.coolDown = 0;
            this.dodgeChance = 0;
            this.crowdControl = 0;
            this.critRate = 0;
            this.critDamage = 0;
        }
    }
}
