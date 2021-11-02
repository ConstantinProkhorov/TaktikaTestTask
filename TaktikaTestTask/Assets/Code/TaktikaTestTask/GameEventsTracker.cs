using Code.TaktikaTestTask.Hero.Messages;
using Code.TaktikaTestTask.UI.Messages;
using Code.TaktikaTestTask.UI.Screens;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask
{
    public class GameEventsTracker : MonoBehaviour
    {
        private void Awake()
        {
            Bind();
        }

        private void Bind()
        {
            MessageBroker.Default.Receive<HeroKilledMessage>()
                .Take(1)
                .Subscribe(_ => DoOnHeroDeath())
                .AddTo(this);
        }

        private void DoOnHeroDeath()
        {
            MessageBroker.Default.Publish(new SwitchScreenMessage(ScreenName.DefeatScreen, true));
        }
    }
}