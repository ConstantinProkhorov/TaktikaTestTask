using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.TaktikaTestTask.Utility
{
    public class SceneReload : MonoBehaviour
    {
        private void Awake()
        {
            Bind();
        }

        public static void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void Bind()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.R))
                .Subscribe(_ => Reload())
                .AddTo(this);
        }
    }
}