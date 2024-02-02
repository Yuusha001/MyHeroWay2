namespace MyHeroWay
{
    public interface ICharacterStats 
    {
        public CharacterStats originalStats { get; }
        public CharacterStats runtimeStats { get;  }

        public event System.Action OnCrit;
        public void ReduceHP(int amount);
        public void ReduceMP(int amount);
        public void GainMP(int amount);
        public void GainHP(int amount);
    }
}
