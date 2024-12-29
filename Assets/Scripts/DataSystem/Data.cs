using LanguageSystem;

namespace DataSystem
{
    /// <summary>
    /// This class represents the data used in the game.
    /// It includes various settings and information that will be saved and loaded.
    /// </summary>
    [System.Serializable]
    public class Data
    {
        public Language gameLanguage;

        public Data()
        {
            gameLanguage = Language.EN;
        }
    }
}