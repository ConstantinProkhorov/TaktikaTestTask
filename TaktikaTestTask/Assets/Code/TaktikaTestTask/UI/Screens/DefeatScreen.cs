using Code.TaktikaTestTask.Hero.Messages;
using Code.TaktikaTestTask.Utility;
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
            
            restartGameButton.onClick.AddListener(SceneReload.Reload);
        }

        private void OnDestroy()
        {
            restartGameButton.onClick.RemoveAllListeners();
        }

        private void Bind()
        {
            MessageBroker.Default.Receive<TotalEnemiesKilledMessage>()
                .Subscribe(m => enemiesKilledText.text = m.Count.ToString())
                .AddTo(this);
        }
    }
}