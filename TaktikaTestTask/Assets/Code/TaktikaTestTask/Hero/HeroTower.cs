using System.Collections.Generic;
using System.Linq;
using Code.TaktikaTestTask.Enemies.Messages;
using Code.TaktikaTestTask.GameSettings;
using Code.TaktikaTestTask.Hero.Messages;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Hero
{
    [DisallowMultipleComponent]
    public class HeroTower : MonoBehaviour
    {
        [SerializeField] private HeroSettings settings;
        [SerializeField] private float debrisPushForce = 10f;

        public readonly ReactiveProperty<int> CurrentHealth = new ReactiveProperty<int>();

        private List<Rigidbody> _parts = new List<Rigidbody>();

        private void Awake()
        {
            CurrentHealth.Value = settings.StartingHealth;
            _parts = GetComponentsInChildren<Rigidbody>().ToList();
            Bind();
        }

        private void Bind()
        {
            MessageBroker.Default.Receive<EnemyDidDamageMessage>()
                .Select(m =>
                {
                    print("ffff");
                    return CurrentHealth.Value -= m.Damage;
                })
                .Where(h => h <= 0)
                .Subscribe(_ => EndGame())
                .AddTo(this);
            
            MessageBroker.Default.Publish(new HeroHealthCounterMessage(CurrentHealth));
        }

        private void EndGame()
        {
            MessageBroker.Default.Publish(new HeroKilledMessage());
            KillTower();
        }

        private void KillTower()
        {
            _parts.ForEach(r =>
            {
                r.isKinematic = false;
                var pushDirection = (r.transform.position - transform.position).normalized;
                r.AddForce(pushDirection * debrisPushForce, ForceMode.Impulse);
            });
        }
    }
}