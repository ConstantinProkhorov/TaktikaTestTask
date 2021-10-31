using System;
using Code.TaktikaTestTask.WayPoints;
using UnityEngine;

namespace Code.TaktikaTestTask.Enemies.Movement
{
    [DisallowMultipleComponent]
    public class EnemyMovementData : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        private WayPointsDistributor _wayPointsDistributor;

        public IWayPoint TargetWayPoint { get; private set; }
        public Vector3 RandomDeviation { get; private set; }

        public float Speed => speed;
        
        public event Action EndedMovement = delegate { };

        private void OnDestroy()
        {
            EndedMovement = null;
        }

        public void Initialize(WayPointsDistributor pointsDistributor, Vector3 deviation)
        {
            _wayPointsDistributor = pointsDistributor;
            RandomDeviation = deviation;
            TargetWayPoint = _wayPointsDistributor.GetPointWithIndex(0);
        }

        public void SetNextPoint()
        {
            var newWayPoint = _wayPointsDistributor.GetPointWithIndex(TargetWayPoint.PointID + 1);
            
            if (newWayPoint is NullWayPoint)
            {
                EndMovement();
            }
            else
            {
                TargetWayPoint = newWayPoint;
            }
        }

        public void EndMovement()
        {
            EndedMovement?.Invoke();
            EndedMovement = null;
        }
    }
}