using Localization.Scripts;

namespace DataSystem.Scripts
{
    /// <summary>
    /// Data class represents the data used in the game.
    /// It includes various settings and information that will be saved and loaded using <see cref="DataManager"/>.
    /// </summary>
    [System.Serializable]
    public class Data
    {
        public string selectedLocaleCode;

        public Data()
        {
            selectedLocaleCode = LocalizationManager.DefaultLocaleCode;
        }
    }
}