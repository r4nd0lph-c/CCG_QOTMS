using System.IO;
using UnityEngine;

namespace DataSystem
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        private const string DataFile = "Data.json";

        void Awake()
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

        public void SaveData(Data data)
        {
            string serializedData = JsonUtility.ToJson(data);

#if UNITY_WEBGL
            PlayerPrefs.SetString(DataFile, serializedData);
            PlayerPrefs.Save();
#elif UNITY_STANDALONE
            string path = Path.Combine(Application.persistentDataPath, DataFile);
            File.WriteAllText(path, serializedData);
#endif
        }

        public Data LoadData()
        {
            string serializedData = string.Empty;

#if UNITY_WEBGL
            if (PlayerPrefs.HasKey(DataFile))
                serializedData = PlayerPrefs.GetString(DataFile);
#elif UNITY_STANDALONE
            string path = Path.Combine(Application.persistentDataPath, DataFile);
            if (File.Exists(path))
                serializedData = File.ReadAllText(path);
#endif

            if (string.IsNullOrEmpty(serializedData))
                return new Data();
            return JsonUtility.FromJson<Data>(serializedData);
        }
    }
}