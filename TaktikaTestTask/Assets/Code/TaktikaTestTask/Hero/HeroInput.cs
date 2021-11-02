using Code.TaktikaTestTask.Hero.DefenceTowers;
using Code.TaktikaTestTask.Utility;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Code.TaktikaTestTask.Hero
{
    [DisallowMultipleComponent]
    public class HeroInput : MonoBehaviour
    {
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
            Bind();   
        }

        private void Bind()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => RaycastForTower())
                .AddTo(this);
        }

        private void RaycastForTower()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var layerMask = 1 << Layers.DefenceTowerLayer;
            RaycastHit[] results = new RaycastHit[1];
            var hits = Physics.RaycastNonAlloc(ray, results, float.MaxValue, layerMask);
            if (hits > 0)
            {
                var defenceTower = results[0].collider.GetComponent<DefenceTower>();
                if (!defenceTower) return;
                defenceTower.Upgrade();
            }
        }
    }
}