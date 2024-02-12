namespace MyHeroWay
{
    public struct DamageResult
    {
        public float damage;
        public bool isCrit;
        public bool isMiss;
        public bool canKnockBack;
        public EDamageType damageType;

        public DamageResult(float damage, bool isCrit, bool isMiss, bool canKnockBack, EDamageType damageType)
        {
            this.damage = damage;
            this.isCrit = isCrit;
            this.isMiss = isMiss;
            this.canKnockBack = canKnockBack;
            this.damageType = damageType;
        }
    }
}
