using Code.TaktikaTestTask.Hero.Messages;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GoldTextUpdater : MonoBehaviour
    {
        [SerializeField] private float updateAnimationStrength = 1.2f;
        [SerializeField] private float updateAnimationDuration = 0.3f;
        [SerializeField] private Ease updateAnimationEase = Ease.OutElastic;

        private Vector3 _textInitialScale;
        
        private void Awake()
        {
            Bind(GetComponent<TextMeshProUGUI>());
        }

        private void Bind(TMP_Text text)
        {
            _textInitialScale = text.rectTransform.localScale;
            MessageBroker.Default.Receive<HeroGoldCounterMessage>()
                .Take(1)
                .Subscribe(m =>
                {
                    m.Gold
                        .Subscribe(i => UpdateDisplay(i, text))
                        .AddTo(this);
                })
                .AddTo(this);
        }

        private void UpdateDisplay(int newValue, TMP_Text text)
        {
            text.text = newValue.ToString();
            text.transform
                .DOPunchScale(Vector3.one * updateAnimationStrength, updateAnimationDuration)
                .SetEase(updateAnimationEase)
                .OnComplete(() => text.rectTransform.localScale = _textInitialScale)
                .Play();
        }
    }
}