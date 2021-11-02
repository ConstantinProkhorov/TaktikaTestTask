using Code.TaktikaTestTask.Hero.Messages;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code.TaktikaTestTask.UI.Screens
{
    public class DefeatScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemiesKilledText;
        [SerializeField] private Button restartGameButton;

        private void Awake()
        {
            Bind();
        }

        private void Bind()
        {
            print("binding");
            MessageBroker.Default.Receive<TotalEnemiesKilledMessage>()
                .Do(_ => print(2))
                .Subscribe(m => enemiesKilledText.text = m.Count.ToString())
                .AddTo(this);
        }
    }
}