using System;
using Code.TaktikaTestTask.Enemies.Mover;
using Code.TaktikaTestTask.GameSettings;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Enemies.Creator
{
    [DisallowMultipleComponent]
    public class WaveCreator : MonoBehaviour
    {
        private SpawnPoint _spawnPoint;
        private int _currentWaveNumber;
        
        public void Initialize(SpawnPoint spawnPoint, EnemyCreator enemyCreator, EnemyMover enemyMover, 
            EnemiesSpawnSettings settings)
        {
            _spawnPoint = spawnPoint;
            
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(settings.IntervalBetweenWaves))
                .Subscribe(_ => SpawnWave(enemyCreator, enemyMover, settings))
                .AddTo(this);
        }

        private async void SpawnWave(EnemyCreator enemyCreator, EnemyMover enemyMover, EnemiesSpawnSettings settings)
        {
            var waveCount = CalculateWaveCount(settings);
            for (int i = 0; i < waveCount; i++)
            {
                var newEnemy = enemyCreator.Create(_spawnPoint);
                enemyMover.AddEnemy(newEnemy);
                await UniTask.Delay(TimeSpan.FromSeconds(settings.IntervalBetweenEnemies),
                    cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            _currentWaveNumber++;
        }

        private int CalculateWaveCount(EnemiesSpawnSettings settings)
        {
            return _currentWaveNumber + settings.AdditionalEnemiesPerWave;
        }
    }
}