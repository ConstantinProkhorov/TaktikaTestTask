namespace Code.TaktikaTestTask.Enemies.Messages
{
    public readonly struct EnemyDidDamageMessage
    {
        public EnemyDidDamageMessage(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; }
    }
}