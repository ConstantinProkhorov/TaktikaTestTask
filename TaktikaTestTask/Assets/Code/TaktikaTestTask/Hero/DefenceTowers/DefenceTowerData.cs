using Code.TaktikaTestTask.GameSettings;

namespace Code.TaktikaTestTask.Hero.DefenceTowers
{
    public class DefenceTowerData
    {
        public int Damage { get; }
        public double DelayBetweenShots { get; }
        
        public DefenceTowerData(int initialDamage, double initialDelayBetweenShots)
        {
            Damage = initialDamage;
            DelayBetweenShots = initialDelayBetweenShots;
        }

        public static DefenceTowerData DefaultFromSettings(DefenceTowerSettings settings)
        {
            return new DefenceTowerData(settings.InitialDamage, settings.InitialDelayBetweenShots);
        }

        public override string ToString()
        {
            return $"(Damage: {Damage}, Delay {DelayBetweenShots})";
        }
    }
}