using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Enemies.Movement
{
    public class EnemyMover
    {
        private readonly List<EnemyMovementData> _enemiesToMove = new List<EnemyMovementData>();

        public EnemyMover(Component obj)
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    for (int i = 0; i < _enemiesToMove.Count; i++)
                    {
                        MoveEnemy(_enemiesToMove[i]);
                    }
                })
                .AddTo(obj);
        }

        public void AddEnemy(EnemyMovementData enemyMovementData)
        {
            if (_enemiesToMove.Contains(enemyMovementData)) return;
            _enemiesToMove.Add(enemyMovementData);

            enemyMovementData.EndedMovement += () => _enemiesToMove.Remove(enemyMovementData);
        }

        private static void MoveEnemy(EnemyMovementData enemyMovementData)
        {
            var currentTarget = enemyMovementData.TargetWayPoint.Transform.position + enemyMovementData.RandomDeviation;

            var direction = currentTarget - enemyMovementData.transform.position;
            var distanceToTarget = direction.sqrMagnitude;
            var move = direction.normalized * (enemyMovementData.Speed * Time.deltaTime);
            if (distanceToTarget < move.sqrMagnitude)
            {
                enemyMovementData.transform.position = currentTarget;
                enemyMovementData.SetNextPoint();
                return;
            }
            
            enemyMovementData.transform.position += move;
            enemyMovementData.transform.forward = direction;
        }
    }
}