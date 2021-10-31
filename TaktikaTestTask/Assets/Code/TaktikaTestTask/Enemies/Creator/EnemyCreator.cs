using Code.TaktikaTestTask.Enemies.Movement;
using Code.TaktikaTestTask.GameSettings;
using Code.TaktikaTestTask.Pool;
using Code.TaktikaTestTask.WayPoints;
using UnityEngine;

namespace Code.TaktikaTestTask.Enemies.Creator
{
    [DisallowMultipleComponent]
    public class EnemyCreator : MonoBehaviour
    {
        private readonly System.Random _random = new System.Random();
        
        private Pool<Enemy> _pool;
        private WayPointsDistributor _wayPointsDistributor;
        private EnemySettings _enemySettings;
        private EnemyData _currentEnemyData;
        
        public void Initialize(WayPointsDistributor wayPointsDistributor, EnemySettings enemySettings, EnemiesSpawnSettings spawnSettings)
        {
            _wayPointsDistributor = wayPointsDistributor;
            _enemySettings = enemySettings;
            _currentEnemyData = EnemyData.DefaultFromSettings(enemySettings);
            _pool = new Pool<Enemy>(spawnSettings.EnemyPoolStartingSize, spawnSettings.EnemyMovementDataPrefab, transform);
        }

        public EnemyMovementData Create(SpawnPoint spawnPoint)
        {
            var newEnemy = _pool.GetFromPool();
            newEnemy.Killed += () => _pool.ReturnToPool(newEnemy);
            
            var randomDeviation = CalculateRandomDeviation(spawnPoint);
            newEnemy.transform.position = spawnPoint.Position + randomDeviation;
            newEnemy.gameObject.SetActive(true);
            newEnemy.Initialize(_currentEnemyData, _wayPointsDistributor, randomDeviation);
            return newEnemy.MovementData;
        }
        
        public void RecalculateEnemyDataForNewWave()
        {
            (bool updateHealth, bool updateDamage, bool updateReward) = GetRandomUpgrades();
            var newHealth = updateHealth 
                ? _currentEnemyData.Health + _enemySettings.HealthStepPerUpgrade 
                : _currentEnemyData.Health;
            var newDamage = updateDamage 
                ? _currentEnemyData.Damage + _enemySettings.DamageStepPerUpgrade 
                : _currentEnemyData.Damage;
            var newGoldReward = updateReward 
                ? _currentEnemyData.GoldReward + _enemySettings.RewardStepPerUpgrade 
                : _currentEnemyData.GoldReward;
            
            _currentEnemyData = new EnemyData(newHealth, newDamage, newGoldReward);
        }
        
        private Vector3 CalculateRandomDeviation(SpawnPoint spawnPoint)
        {
            var deviation = Random.insideUnitCircle * spawnPoint.Radius;
            return new Vector3(deviation.x, 0f, deviation.y);
        }

        private (bool, bool, bool) GetRandomUpgrades()
        {
            return (_random.Next(2) == 0, _random.Next(2) == 0, _random.Next(2) == 0);
        }
    }
}