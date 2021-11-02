using Code.TaktikaTestTask.UI.Screens;

namespace Code.TaktikaTestTask.UI.Messages
{
    public readonly struct SwitchScreenMessage
    {
        public SwitchScreenMessage(ScreenName screenName, bool activation)
        {
            ScreenName = screenName;
            Activation = activation;
        }

        public ScreenName ScreenName { get; }
        public bool Activation { get; }
    }
}