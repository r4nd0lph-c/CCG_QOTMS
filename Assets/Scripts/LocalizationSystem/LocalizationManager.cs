using UnityEngine;

namespace LocalizationSystem
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}