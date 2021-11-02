using Code.TaktikaTestTask.WayPoints.Messages;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.WayPoints
{
    [DefaultExecutionOrder(0)]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Renderer))]
    public class WayPoint : MonoBehaviour, IWayPoint
    {
        [SerializeField] private int pointID;

        public int PointID => pointID;
        public Transform Transform => transform;

        private void Awake()
        {
            MessageBroker.Default.Publish(new WayPointMessage(this));
        }
    }
}