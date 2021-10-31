using System;
using Code.TaktikaTestTask.Enemies.Movement;
using Code.TaktikaTestTask.GameSettings;
using Code.TaktikaTestTask.Hero.Messages;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.TaktikaTestTask.Enemies.Creator
{
    [DisallowMultipleComponent]
    public class WaveCreator : MonoBehaviour
    {
        private SpawnPoint _spawnPoint;
        private int _currentWaveNumber = 1;
        
        public void Initialize(SpawnPoint spawnPoint, EnemyCreator enemyCreator, EnemyMover enemyMover, 
            EnemiesSpawnSettings settings)
        {
            var disposable = new CompositeDisposable();
            _spawnPoint = spawnPoint;
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(settings.IntervalBetweenWaves))
                .Subscribe(_ => SpawnWave(enemyCreator, enemyMover, settings))
                .AddTo(disposable);

            MessageBroker.Default.Receive<HeroKilledMessage>()
                .Take(1)
                .Subscribe(_ => disposable.Clear())
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
            enemyCreator.RecalculateEnemyDataForNewWave();
        }

        private int CalculateWaveCount(EnemiesSpawnSettings settings)
        {
            var waveMax = _currentWaveNumber + settings.AdditionalEnemiesPerWave;
            var result = Random.Range(_currentWaveNumber, waveMax + 1);
            return result;
        }
    }
}