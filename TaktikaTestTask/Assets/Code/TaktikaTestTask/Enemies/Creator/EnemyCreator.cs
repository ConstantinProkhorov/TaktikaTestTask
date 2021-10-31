using Code.TaktikaTestTask.GameSettings;
using Code.TaktikaTestTask.Pool;
using Code.TaktikaTestTask.WayPoints;
using UnityEngine;

namespace Code.TaktikaTestTask.Enemies.Creator
{
    [DisallowMultipleComponent]
    public class EnemyCreator : MonoBehaviour
    {
        private Pool<Enemy> _pool;
        private WayPointsDistributor _wayPointsDistributor;
        
        public void Initialize(WayPointsDistributor wayPointsDistributor, EnemiesSpawnSettings settings)
        {
            _wayPointsDistributor = wayPointsDistributor;
            
            _pool = new Pool<Enemy>(settings.EnemyPoolStartingSize, settings.EnemyPrefab, transform);
        }

        // add here enemy stats.
        public Enemy Create(SpawnPoint spawnPoint)
        {
            var newEnemy = _pool.GetFromPool();

            var randomDeviation = CalculateRandomDeviation(spawnPoint);
            newEnemy.transform.position = spawnPoint.Position + randomDeviation;
            newEnemy.gameObject.SetActive(true);
            newEnemy.Initialize(_wayPointsDistributor, randomDeviation);
            // тут инициализация врага
            return newEnemy;
        }
        
        private Vector3 CalculateRandomDeviation(SpawnPoint spawnPoint)
        {
            var deviation = Random.insideUnitCircle * spawnPoint.Radius;
            return new Vector3(deviation.x, 0f, deviation.y);
        }
    }
}