namespace MyHeroWay
{
    public interface IDamage
    {
        public Controller controller { get; }
        public void TakeDamage(DamageInfo damageInfo, System.Action callBack = null);
    }
}