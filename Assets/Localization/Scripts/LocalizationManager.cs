using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using DataSystem.Scripts;

namespace Localization.Scripts
{
    /// <summary>
    /// LocalizationManager class handles localization by loading and changing the locale
    /// based on game data provided by <see cref="DataManager"/>.
    /// </summary>
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance { get; private set; }
        public const string DefaultLocaleCode = "en";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                DataManager.Instance.OnDataLoaded += LoadLocale;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private static Locale LocaleByCode(string code)
        {
            Locale locale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.Identifier.Code == code);
            return locale != null ? locale : LocaleByCode(DefaultLocaleCode);
        }

        private void LoadLocale()
        {
            LocalizationSettings.SelectedLocale = LocaleByCode(DataManager.Instance.data.selectedLocaleCode);
        }

        public static void ChangeLocale(string code)
        {
            Locale locale = LocaleByCode(code);
            DataManager.Instance.data.selectedLocaleCode = locale.Identifier.Code;
            DataManager.Instance.Save();
            LocalizationSettings.SelectedLocale = locale;
        }
    }
}