using UnityEngine;

namespace Code.TaktikaTestTask.WayPoints
{
    public interface IWayPoint
    {
        int PointID { get; }
        Transform Transform { get; }
    }
}