using UnityEngine;

namespace Code.TaktikaTestTask.GameSettings
{
    [CreateAssetMenu(fileName = "DefenceTowerSettings", menuName = "TaktikaTestTask/DefenceTowerSettings", order = 2)]
    public class DefenceTowerSettings : ScriptableObject
    {
        [SerializeField] private int initialDamage = 1;
        [SerializeField] private double initialDelayBetweenShots = 2;
        [SerializeField] private int initialUpgradePrice = 10;
        [SerializeField] private int damageStepPerUpgrade = 1;
        [SerializeField] private double delayBetweenShotsStepPerUpgrade = 0.2;
        [SerializeField] private int upgradeCostStepPerUpgrade = 20;
        [SerializeField] private double minimalDelayBetweenShots = 0.2;

        public int InitialDamage => initialDamage;
        public double InitialDelayBetweenShots => initialDelayBetweenShots;
        public int InitialUpgradePrice => initialUpgradePrice;
        public int DamageStepPerUpgrade => damageStepPerUpgrade;
        public double DelayBetweenShotsStepPerUpgrade => delayBetweenShotsStepPerUpgrade;
        public int UpgradeCostStepPerUpgrade => upgradeCostStepPerUpgrade;
        public double MinimalDelayBetweenShots => minimalDelayBetweenShots;
    }
}