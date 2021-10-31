using System.Linq;
using UnityEngine;

namespace Code.TaktikaTestTask.Utility
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Renderer))]
    public class DisableRenderersOnAwake : MonoBehaviour
    {
        [SerializeField] private bool shouldDisable = true;
        
        private void Awake()
        {
            if (shouldDisable)
            {
                GetComponentsInChildren<Renderer>()
                    .ToList()
                    .ForEach(r => r.enabled = false);
            }    
        }
    }
}