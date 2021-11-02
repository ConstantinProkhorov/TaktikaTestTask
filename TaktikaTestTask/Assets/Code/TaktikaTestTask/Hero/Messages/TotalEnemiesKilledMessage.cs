namespace Code.TaktikaTestTask.Hero.Messages
{
    public readonly struct TotalEnemiesKilledMessage
    {
        public TotalEnemiesKilledMessage(int count)
        {
            Count = count;
        }

        public int Count { get; }
    }
}