using UnityEngine;

namespace Code.TaktikaTestTask.GameSettings
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "TaktikaTestTask/EnemySettings", order = 1)]
    public class EnemySettings : ScriptableObject
    {
        [SerializeField] private int initialHealth;
        [SerializeField] private int initialDamage;
        [SerializeField] private int initialGoldReward;
        [SerializeField] private int healthStepPerUpgrade;
        [SerializeField] private int damageStepPerUpgrade;
        [SerializeField] private int rewardStepPerUpgrade;

        public int InitialHealth => initialHealth;
        public int InitialDamage => initialDamage;
        public int InitialGoldReward => initialGoldReward;
        public int HealthStepPerUpgrade => healthStepPerUpgrade;
        public int DamageStepPerUpgrade => damageStepPerUpgrade;
        public int RewardStepPerUpgrade => rewardStepPerUpgrade;
    }
}