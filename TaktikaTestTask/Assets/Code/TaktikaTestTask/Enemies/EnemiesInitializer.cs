using Code.TaktikaTestTask.Enemies.Creator;
using Code.TaktikaTestTask.Enemies.Movement;
using Code.TaktikaTestTask.GameSettings;
using Code.TaktikaTestTask.WayPoints;
using UnityEngine;

namespace Code.TaktikaTestTask.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(WayPointsDistributor))]
    [RequireComponent(typeof(EnemyCreator), typeof(WaveCreator))]
    public class EnemiesInitializer : MonoBehaviour
    {
        [SerializeField] private EnemySettings enemySettings;
        [SerializeField] private EnemiesSpawnSettings spawnSettings;
        [SerializeField] private SpawnPoint spawnPoint;

        private void Start()
        {
            var wayPointsDistributor = GetComponent<WayPointsDistributor>();
            var enemyMover = new EnemyMover(this);
            var enemyCreator = GetComponent<EnemyCreator>();
            var waveCreator = GetComponent<WaveCreator>();
            
            enemyCreator.Initialize(wayPointsDistributor, enemySettings, spawnSettings);
            waveCreator.Initialize(spawnPoint, enemyCreator, enemyMover, spawnSettings);
        }
    }
}