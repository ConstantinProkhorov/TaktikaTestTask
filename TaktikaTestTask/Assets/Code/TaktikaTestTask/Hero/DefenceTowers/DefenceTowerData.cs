using Code.TaktikaTestTask.GameSettings;

namespace Code.TaktikaTestTask.Hero.DefenceTowers
{
    public readonly struct DefenceTowerData
    {
        public int Damage { get; }
        public double DelayBetweenShots { get; }
        public int UpgradeCost { get; }
        
        public DefenceTowerData(int damage, double delayBetweenShots, int upgradeCost)
        {
            Damage = damage;
            DelayBetweenShots = delayBetweenShots;
            UpgradeCost = upgradeCost;
        }

        public static DefenceTowerData DefaultFromSettings(DefenceTowerSettings settings)
        {
            return new DefenceTowerData(settings.InitialDamage, settings.InitialDelayBetweenShots, settings.InitialUpgradePrice);
        }

        public override string ToString()
        {
            return $"(Damage: {Damage}, Delay {DelayBetweenShots})";
        }
    }
}