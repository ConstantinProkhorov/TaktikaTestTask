using Code.TaktikaTestTask.GameSettings;

namespace Code.TaktikaTestTask.Enemies
{
    public readonly struct EnemyData
    {
        public int Health { get; }
        public int Damage { get; }
        public int GoldReward { get; }

        public EnemyData(int health, int damage, int goldReward)
        {
            Health = health;
            Damage = damage;
            GoldReward = goldReward;
        }

        public static EnemyData DefaultFromSettings(EnemySettings settings)
        {
            return new EnemyData(settings.InitialHealth, settings.InitialDamage, settings.InitialGoldReward);
        }

        public override string ToString()
        {
            return $"(Health: {Health}, Damage: {Damage}, GoldReward {GoldReward})";
        }
    }
}