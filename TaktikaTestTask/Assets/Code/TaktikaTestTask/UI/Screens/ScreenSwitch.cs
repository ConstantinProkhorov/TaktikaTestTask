using Code.TaktikaTestTask.UI.Messages;
using UniRx;
using UnityEngine;

namespace Code.TaktikaTestTask.UI.Screens
{
    public class ScreenSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject gameScreen;
        [SerializeField] private GameObject defeatScreen;

        private void Start()
        {
            Bind();
            MessageBroker.Default.Publish(new SwitchScreenMessage(ScreenName.GameScreen, true));
        }

        private void Bind()
        {
            MessageBroker.Default.Receive<SwitchScreenMessage>()
                .Where(m => m.ScreenName == ScreenName.GameScreen)
                .Subscribe(m =>
                {
                    TurnAllScreensOff();
                    gameScreen.SetActive(m.Activation);
                })
                .AddTo(this);
            
            MessageBroker.Default.Receive<SwitchScreenMessage>()
                .Where(m => m.ScreenName == ScreenName.DefeatScreen)
                .Subscribe(m =>
                {
                    TurnAllScreensOff();
                    defeatScreen.SetActive(m.Activation);
                })
                .AddTo(this);
        }

        private void TurnAllScreensOff()
        {
            gameScreen.SetActive(false);
            defeatScreen.SetActive(false);
        }
    }
}