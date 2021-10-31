using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Enemies.Mover
{
    public class EnemyMover
    {
        private readonly List<Enemy> _enemiesToMove = new List<Enemy>();

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

        public void AddEnemy(Enemy enemy)
        {
            if (_enemiesToMove.Contains(enemy)) return;
            _enemiesToMove.Add(enemy);

            enemy.EndedMovement += () => _enemiesToMove.Remove(enemy);
        }

        private static void MoveEnemy(Enemy enemy)
        {
            var currentTarget = enemy.TargetWayPoint.Transform.position + enemy.RandomDeviation;

            var direction = currentTarget - enemy.transform.position;
            var distanceToTarget = direction.sqrMagnitude;
            var move = direction.normalized * (enemy.Speed * Time.deltaTime);
            if (distanceToTarget < move.sqrMagnitude)
            {
                enemy.transform.position = currentTarget;
                enemy.SetNextPoint();
                return;
            }
            
            enemy.transform.position += move;
            enemy.transform.forward = direction;
        }
    }
}