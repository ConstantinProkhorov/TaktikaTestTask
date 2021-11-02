using Code.TaktikaTestTask.Hero.Messages;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code.TaktikaTestTask.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    public class HeroHealthSliderUpdater : MonoBehaviour
    {
        private void Awake()
        {
            Bind(GetComponent<Slider>(), GetComponentInChildren<TextMeshProUGUI>());
        }

        private void Bind(Slider slider, TMP_Text text)
        {
            MessageBroker.Default.Receive<HeroHealthCounterMessage>()
                .Take(1)
                .Subscribe(m =>
                {
                    slider.maxValue = m.Health.Value;
                    UpdateSlider((int)slider.maxValue, slider, text);
                    m.Health
                        .Subscribe(i => UpdateSlider(i, slider, text))
                        .AddTo(this);
                })
                .AddTo(this);
        }

        private static void UpdateSlider(int newValue, Slider slider, TMP_Text text)
        {
            slider.value = newValue;
            if (!text) return;
            newValue = Mathf.Clamp(newValue, 0, int.MaxValue);
            text.text = newValue.ToString();
        }
    }
}