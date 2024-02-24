namespace MyHeroWay
{
    public struct DamageResult
    {
        public float damage;
        public bool isCrit;
        public bool isMiss;
        public EDamageType damageType;

        public DamageResult(float damage, bool isCrit, bool isMiss, EDamageType damageType)
        {
            this.damage = damage;
            this.isCrit = isCrit;
            this.isMiss = isMiss;
            this.damageType = damageType;
        }
    }
}
