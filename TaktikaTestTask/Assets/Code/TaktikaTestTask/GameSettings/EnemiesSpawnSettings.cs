using Code.TaktikaTestTask.Enemies;
using UnityEngine;

namespace Code.TaktikaTestTask.GameSettings
{
    [CreateAssetMenu(fileName = "WaveSpawnSettings", menuName = "TaktikaTestTask/WaveSpawnSettings", order = 0)]
    public class EnemiesSpawnSettings : ScriptableObject
    {
        [SerializeField] private double intervalBetweenWaves = 10;
        [SerializeField] private double intervalBetweenEnemies = 0.2;
        [SerializeField] private int additionalEnemiesPerWave = 1;
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private int enemyPoolStartingSize = 64;

        public double IntervalBetweenWaves => intervalBetweenWaves;
        public double IntervalBetweenEnemies => intervalBetweenEnemies;
        public int AdditionalEnemiesPerWave => additionalEnemiesPerWave;
        public Enemy EnemyPrefab => enemyPrefab;
        public int EnemyPoolStartingSize => enemyPoolStartingSize;
    }
}