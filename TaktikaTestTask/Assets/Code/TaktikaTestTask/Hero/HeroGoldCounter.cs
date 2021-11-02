using Code.TaktikaTestTask.Enemies.Messages;
using Code.TaktikaTestTask.Hero.Messages;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Hero
{
    [DisallowMultipleComponent]
    public class HeroGoldCounter : MonoBehaviour
    {
        public readonly ReactiveProperty<int> Gold = new ReactiveProperty<int>();
        
        private void Awake()
        {
            Bind();
        }

        private void Start()
        {
            MessageBroker.Default.Publish(new HeroGoldCounterMessage(this));
        }

        public bool TryBuy(int price)
        {
            if (Gold.Value >= price)
            {
                Gold.Value -= price;
                return true;
            }
            return false;
        }

        private void Bind()
        {
            MessageBroker.Default.Receive<EnemyKilledMessage>()
                .Subscribe(m => Gold.Value += m.GoldReward)
                .AddTo(this);
        }
    }
}