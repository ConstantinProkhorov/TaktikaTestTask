using Code.TaktikaTestTask.GameSettings;
using Code.TaktikaTestTask.Hero.Messages;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Hero.DefenceTowers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DefenceTowerShooter))]
    public class DefenceTower : MonoBehaviour
    {
        [SerializeField] private DefenceTowerSettings settings;
        [SerializeField] private float upgradeEffectStrength = 1.2f;
        [SerializeField] private float upgradeEffectDuration = 0.3f;
        [SerializeField] private Ease updateAnimationEase = Ease.OutElastic;

        private DefenceTowerData _currentTowerData;
        private DefenceTowerShooter _defenceTowerShooter;
        private HeroGoldCounter _heroGoldCounter;
        private TextMeshPro _upgradePriceText;

        private void Awake()
        {
            _currentTowerData = DefenceTowerData.DefaultFromSettings(settings);
            
            _defenceTowerShooter = GetComponent<DefenceTowerShooter>();
            _defenceTowerShooter.Initialize(_currentTowerData);
            
            _upgradePriceText = GetComponentInChildren<TextMeshPro>();
            _upgradePriceText.text = _currentTowerData.UpgradeCost.ToString();

            Bind();
        }

        private void Bind()
        {
            MessageBroker.Default.Receive<HeroGoldCounterMessage>()
                .Subscribe(m => _heroGoldCounter = m.GoldCounter)
                .AddTo(this);
        }

        public void Upgrade()
        {
            if (_heroGoldCounter.TryBuy(_currentTowerData.UpgradeCost))
            {
                PlayUpgradeEffect();
                _currentTowerData = CalculateNewDefenceTowerData();
                _defenceTowerShooter.Initialize(_currentTowerData);
                _upgradePriceText.text = _currentTowerData.UpgradeCost.ToString();
            }
        }

        private void PlayUpgradeEffect()
        {
            transform
                .DOPunchScale(Vector3.one * upgradeEffectStrength, upgradeEffectDuration)
                .SetEase(updateAnimationEase)
                .Play();
        }

        private DefenceTowerData CalculateNewDefenceTowerData()
        {
            var newDamage = _currentTowerData.Damage + settings.DamageStepPerUpgrade;
            var newDelay = _currentTowerData.DelayBetweenShots - settings.DelayBetweenShotsStepPerUpgrade;
            if (newDelay < settings.MinimalDelayBetweenShots) newDelay = settings.MinimalDelayBetweenShots;
            var newUpgradePrice = _currentTowerData.UpgradeCost + settings.UpgradeCostStepPerUpgrade;
            
            return new DefenceTowerData(newDamage, newDelay, newUpgradePrice);
        }
    }
}