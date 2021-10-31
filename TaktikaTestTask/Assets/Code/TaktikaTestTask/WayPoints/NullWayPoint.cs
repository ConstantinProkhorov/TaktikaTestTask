using UnityEngine;

namespace Code.TaktikaTestTask.WayPoints
{
    public class NullWayPoint : IWayPoint
    {
        public int PointID => -1;
        public Transform Transform => null;
    }
}