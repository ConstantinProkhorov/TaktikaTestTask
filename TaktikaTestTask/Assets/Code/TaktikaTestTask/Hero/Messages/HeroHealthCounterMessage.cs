using UniRx;

namespace Code.TaktikaTestTask.Hero.Messages
{
    public readonly struct HeroHealthCounterMessage
    {
        public HeroHealthCounterMessage(ReactiveProperty<int> health)
        {
            Health = health;
        }

        public ReactiveProperty<int> Health { get; }
    }
}