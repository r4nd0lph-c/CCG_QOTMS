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
            string encryptedData = AES128.Encrypt(JsonUtility.ToJson(data));

#if UNITY_WEBGL
            PlayerPrefs.SetString(DataFile, encryptedData);
            PlayerPrefs.Save();
#elif UNITY_STANDALONE
            string path = Path.Combine(Application.persistentDataPath, DataFile);
            File.WriteAllText(path, encryptedData);
#endif
        }

        public Data LoadData()
        {
            string encryptedData = string.Empty;

#if UNITY_WEBGL
            if (PlayerPrefs.HasKey(DataFile))
            {
                encryptedData = PlayerPrefs.GetString(DataFile);
            }
#elif UNITY_STANDALONE
            string path = Path.Combine(Application.persistentDataPath, DataFile);
            if (File.Exists(path))
            {
                encryptedData = File.ReadAllText(path);
            }
#endif

            if (string.IsNullOrEmpty(encryptedData))
            {
                return new Data();
            }

            try
            {
                return JsonUtility.FromJson<Data>(AES128.Decrypt(encryptedData));
            }
            catch
            {
                return new Data();
            }
        }
    }
}