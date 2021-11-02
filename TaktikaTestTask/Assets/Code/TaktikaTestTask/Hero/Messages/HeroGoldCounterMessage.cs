using UniRx;

namespace Code.TaktikaTestTask.Hero.Messages
{
    public readonly struct HeroGoldCounterMessage
    {
        public HeroGoldCounterMessage(HeroGoldCounter counter)
        {
            GoldCounter = counter;
            Gold = counter.Gold;
        }

        public HeroGoldCounter GoldCounter { get; }
        public ReactiveProperty<int> Gold { get; }
    }
}