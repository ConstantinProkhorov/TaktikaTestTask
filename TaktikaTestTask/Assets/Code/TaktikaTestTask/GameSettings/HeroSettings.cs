using UnityEngine;

namespace Code.TaktikaTestTask.GameSettings
{
    [CreateAssetMenu(fileName = "HeroSettings", menuName = "TaktikaTestTask/HeroSettings", order = 3)]
    public class HeroSettings : ScriptableObject
    {
        [SerializeField] private int startingHealth = 20;

        public int StartingHealth => startingHealth;
    }
}