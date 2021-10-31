using System.Collections.Generic;
using System.Linq;
using Code.TaktikaTestTask.WayPoints.Messages;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.WayPoints
{
    [DefaultExecutionOrder(-1)]
    [DisallowMultipleComponent]
    public class WayPointsDistributor : MonoBehaviour
    {
        private List<IWayPoint> _wayPoints = new List<IWayPoint>();

        private void Awake()
        {
            MessageBroker.Default.Receive<WayPointMessage>()
                .Subscribe(m => _wayPoints.Add(m.Point))
                .AddTo(this);
        }

        private void Start()
        {
            _wayPoints = _wayPoints.OrderBy(p => p.PointID).ToList();
        }

        public IWayPoint GetPointWithIndex(int index)
        {
            return index >= _wayPoints.Count ? new NullWayPoint() : _wayPoints[index];
        }
    }
}