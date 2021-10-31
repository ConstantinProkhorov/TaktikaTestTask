using UniRx;

namespace Code.TaktikaTestTask.Hero.Messages
{
    public readonly struct HeroGoldCounterMessage
    {
        public HeroGoldCounterMessage(ReactiveProperty<int> gold)
        {
            Gold = gold;
        }

        public ReactiveProperty<int> Gold { get; }
    }
}