using System;
using Code.TaktikaTestTask.Enemies.Messages;
using Code.TaktikaTestTask.Enemies.Movement;
using Code.TaktikaTestTask.WayPoints;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EnemyMovementData))]
    public class Enemy : MonoBehaviour
    {
        private int _currentHealth;

        public EnemyData EnemyData { get; private set; }
        public EnemyMovementData MovementData { get; private set; }
        
        public event Action Killed = delegate { };

        private void Awake()
        {
            MovementData = GetComponent<EnemyMovementData>();
        }

        public void Initialize(EnemyData enemyData, WayPointsDistributor pointsDistributor, Vector3 deviation)
        {
            EnemyData = enemyData;
            _currentHealth = enemyData.Health;
            print(enemyData);
            MovementData.Initialize(pointsDistributor, deviation);
        }

        public void ReceiveDamage(int damage, out bool isKilled)
        {
            _currentHealth -= damage;
            isKilled = _currentHealth <= 0;
            if (isKilled)
            {
                KillEnemy();
            }
        }

        private void DoDamage()
        {
            
        }

        private void KillEnemy()
        {
            Killed?.Invoke();
            Killed = null;
            MovementData.EndMovement();
            MessageBroker.Default.Publish(new EnemyKilledMessage(EnemyData.GoldReward));
        }
    }
}