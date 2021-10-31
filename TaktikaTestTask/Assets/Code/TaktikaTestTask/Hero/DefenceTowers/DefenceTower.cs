using Code.TaktikaTestTask.GameSettings;
using UnityEngine;

namespace Code.TaktikaTestTask.Hero.DefenceTowers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DefenceTowerShooter))]
    public class DefenceTower : MonoBehaviour
    {
        [SerializeField] private DefenceTowerSettings settings;

        private void Awake()
        {
            var defaultTowerData = DefenceTowerData.DefaultFromSettings(settings);
            GetComponent<DefenceTowerShooter>().Initialize(defaultTowerData);
        }
    }
}