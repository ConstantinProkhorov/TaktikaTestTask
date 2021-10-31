using UnityEngine;

namespace Code.TaktikaTestTask.GameSettings
{
    [CreateAssetMenu(fileName = "DefenceTowerSettings", menuName = "TaktikaTestTask/DefenceTowerSettings", order = 3)]
    public class DefenceTowerSettings : ScriptableObject
    {
        [SerializeField] private int initialDamage = 1;
        [SerializeField] private double initialDelayBetweenShots = 2;
        [SerializeField] private double shotEffectShowTime = 0.1;

        public int InitialDamage => initialDamage;
        public double InitialDelayBetweenShots => initialDelayBetweenShots;
        public double ShotEffectShowTime => shotEffectShowTime;
    }
}