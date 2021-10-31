using UnityEngine;

namespace Code.TaktikaTestTask.Enemies.Creator
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Renderer))]
    public class SpawnPoint : MonoBehaviour
    {
        public Vector3 Position { get; private set; }
        public float Radius { get; private set; }

        private void Awake()
        {
            Position = transform.position;
            Radius = GetComponent<Renderer>().bounds.extents.x;
        }
    }
}