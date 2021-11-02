namespace Code.TaktikaTestTask.Enemies.Messages
{
    public readonly struct EnemyKilledMessage
    {
        public EnemyKilledMessage(int goldReward)
        {
            GoldReward = goldReward;
        }

        public int GoldReward { get; }
    }
}