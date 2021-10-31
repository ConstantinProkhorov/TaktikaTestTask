using System;
using Code.TaktikaTestTask.Enemies;
using Code.TaktikaTestTask.Utility;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.Hero.DefenceTowers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(LineRenderer))]
    public class DefenceTowerShooter : MonoBehaviour
    {
        [SerializeField] private double shotEffectShowTime = 0.2;
        
        private LineRenderer _lineRenderer;
        private Enemy _currentTarget;
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void Initialize(DefenceTowerData data)
        {
            Observable.Interval(TimeSpan.FromSeconds(data.DelayBetweenShots))
                .Subscribe(_ => Fire(data))
                .AddTo(this);
        }

        private Enemy SelectTarget()
        {
            if (_currentTarget) return _currentTarget;

            Collider[] hitColliders = new Collider[1];
            const int layerMask = 1 << Layers.EnemyLayer;
            var i = Physics.OverlapSphereNonAlloc(transform.position, float.MaxValue, hitColliders, layerMask);
            
            return i == 0 ? null : hitColliders[0].GetComponent<Enemy>();
        }

        private void Fire(DefenceTowerData data)
        {
            _currentTarget = SelectTarget();
            if (_currentTarget is null) return;

            _currentTarget.ReceiveDamage(data.Damage, out bool isKilled);
            ShowShotEffect(_currentTarget.transform.position);
            if (isKilled)
            {
                _currentTarget = null;
            }
        }

        private async void ShowShotEffect(Vector3 targetPosition)
        {
            _lineRenderer.enabled = true;
            var positions = new[] {transform.position, targetPosition};
            _lineRenderer.SetPositions(positions);

            await UniTask.Delay(TimeSpan.FromSeconds(shotEffectShowTime), cancellationToken: this.GetCancellationTokenOnDestroy());
            _lineRenderer.enabled = false;
        }
    }
}