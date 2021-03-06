using System;
using Code.TaktikaTestTask.Enemies.Messages;
using Code.TaktikaTestTask.Enemies.Movement;
using Code.TaktikaTestTask.WayPoints;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EnemyMovementData))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float deathEffectDuration = 0.5f;
        
        private int _currentHealth;
        private Renderer _renderer;
        private EnemyData _enemyData;
        
        public EnemyMovementData MovementData { get; private set; }
        
        public event Action Killed = delegate { };

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            MovementData = GetComponent<EnemyMovementData>();
            MovementData.ReachedFinalPoint += DoDamage;
        }

        private void OnDestroy()
        {
            MovementData.ReachedFinalPoint -= DoDamage;
        }

        public void Initialize(EnemyData enemyData, WayPointsDistributor pointsDistributor, Vector3 deviation)
        {
            _enemyData = enemyData;
            _currentHealth = enemyData.Health;
            MovementData.Initialize(pointsDistributor, deviation);
        }

        public void ReceiveDamage(int damage, out bool isKilled)
        {
            _currentHealth -= damage;
            isKilled = _currentHealth <= 0;
            if (isKilled)
            {
                KillEnemy();
                MessageBroker.Default.Publish(new EnemyKilledMessage(_enemyData.GoldReward));
            }
        }

        private void DoDamage()
        {
            var initialColor = _renderer.material.color;
            _renderer.material.DOColor(Color.red, deathEffectDuration)
                .OnComplete(() =>
                {
                    KillEnemy();
                    MessageBroker.Default.Publish(new EnemyDidDamageMessage(_enemyData.Damage));
                    _renderer.material.color = initialColor;
                })
                .Play();
        }

        private void KillEnemy()
        {
            Killed?.Invoke();
            Killed = null;
            MovementData.EndMovement();
        }
    }
}