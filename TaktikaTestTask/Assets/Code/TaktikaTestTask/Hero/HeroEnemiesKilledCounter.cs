using Code.TaktikaTestTask.Enemies.Messages;
using Code.TaktikaTestTask.Hero.Messages;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Hero
{
    public class HeroEnemiesKilledCounter : MonoBehaviour
    {
        private int _totalEnemiesKilled;

        private void Awake()
        {
            Bind();
        }

        private void Bind()
        {
            MessageBroker.Default.Receive<EnemyKilledMessage>()
                .Subscribe(_ => _totalEnemiesKilled++)
                .AddTo(this);

            MessageBroker.Default.Receive<HeroKilledMessage>()
                .Take(1)
                .DelayFrame(1)
                .Subscribe(_ => MessageBroker.Default.Publish(new TotalEnemiesKilledMessage(_totalEnemiesKilled)))
                .AddTo(this);
        }
    }
}