namespace Code.TaktikaTestTask.WayPoints.Messages
{
    public readonly struct WayPointMessage
    {
        public WayPointMessage(WayPoint point)
        {
            Point = point;
        }

        public WayPoint Point { get; }
    }
}