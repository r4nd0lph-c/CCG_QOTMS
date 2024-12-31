using System;
using System.IO;
using UnityEngine;

namespace DataSystem.Scripts
{
    /// <summary>
    /// DataManager class manages the saving and loading of game data
    /// with encryption and decryption using <see cref="AES128"/>.
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        private const string DataFile = "Data.json";
        public Data data;
        public event Action OnDataSaved;
        public event Action OnDataLoaded;

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

        private void Start()
        {
            Load();
        }

        public void Save()
        {
            string encryptedData = AES128.Encrypt(JsonUtility.ToJson(data));

#if UNITY_WEBGL
            PlayerPrefs.SetString(DataFile, encryptedData);
            PlayerPrefs.Save();
#elif UNITY_STANDALONE
            string path = Path.Combine(Application.persistentDataPath, DataFile);
            File.WriteAllText(path, encryptedData);
#endif

            OnDataSaved?.Invoke();
        }

        private void Load()
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
                data = new Data();
            }

            try
            {
                data = JsonUtility.FromJson<Data>(AES128.Decrypt(encryptedData));
            }
            catch
            {
                data = new Data();
            }

            OnDataLoaded?.Invoke();
        }
    }
}