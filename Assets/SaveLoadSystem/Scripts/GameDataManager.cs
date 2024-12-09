using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace SpaceSaveLoadSystem
{
    public class GameDataManager : MonoBehaviour
    {
        public Dictionary<string, GameData> gameDataDictionary = new Dictionary<string, GameData>();
        private string _filePath;
        private string _encryptionKey = "mi_clave_segura";
        public static GameDataManager Instance;

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

            _filePath = Application.persistentDataPath + "/gamedata.json";
            LoadData();
        }

        // Método para generar clave compuesta
        public string GenerateKey(int typeData, int numberLevel)
        {
            return $"{typeData}_{numberLevel}";
        }

        // Método para agregar o actualizar datos
        public void AddOrUpdateGameData(int typeData, int numberLevel, int coins, float timePlayed)
        {
            string key = GenerateKey(typeData, numberLevel);

            if (gameDataDictionary.TryGetValue(key, out GameData existingData))
            {
                existingData.coins = coins;
                existingData.timePlayed = timePlayed;
            }
            else
            {
                GameData newData = new GameData
                {
                    typeData = typeData,
                    numberLevel = numberLevel,
                    coins = coins,
                    timePlayed = timePlayed
                };

                gameDataDictionary[key] = newData;
            }

            SaveData();
        }

        // Guardar los datos en un archivo JSON cifrado
        public void SaveData()
        {
            GameDataList dataList = new GameDataList { dataList = new List<GameData>(gameDataDictionary.Values) };
            string json = JsonUtility.ToJson(dataList, true);
            string encryptedJson = Encrypt(json, _encryptionKey);

            File.WriteAllText(_filePath, encryptedJson);
        }

        public void LoadData()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    string encryptedJson = File.ReadAllText(_filePath);
                    string decryptedJson = Decrypt(encryptedJson, _encryptionKey);

                    GameDataList dataList = JsonUtility.FromJson<GameDataList>(decryptedJson);

                    gameDataDictionary.Clear();
                    foreach (GameData data in dataList.dataList)
                    {
                        string key = GenerateKey(data.typeData, data.numberLevel);
                        gameDataDictionary[key] = data;
                    }

                }
                catch (Exception e)
                {
                    Debug.LogError("Error al cargar los datos: " + e.Message);
                }
            }
            else
            {
                Debug.Log("No se encontraron datos previos.");
            }
        }

        private string Encrypt(string plainText, string key)
        {
            byte[] keyBytes = GetKeyBytes(key);
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.GenerateIV();
                byte[] iv = aes.IV;

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                byte[] result = new byte[iv.Length + encryptedBytes.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);

                return Convert.ToBase64String(result);
            }
        }

        private string Decrypt(string cipherText, string key)
        {
            byte[] keyBytes = GetKeyBytes(key);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                byte[] iv = new byte[16];
                byte[] encryptedData = new byte[cipherBytes.Length - iv.Length];

                Buffer.BlockCopy(cipherBytes, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(cipherBytes, iv.Length, encryptedData, 0, encryptedData.Length);

                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] plainBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                return Encoding.UTF8.GetString(plainBytes);
            }
        }

        private byte[] GetKeyBytes(string key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
        }

        public void ResetData()
        {
            gameDataDictionary.Clear();

            if (File.Exists(_filePath))
            {
                try
                {
                    File.Delete(_filePath);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error al eliminar el archivo de datos: " + e.Message);
                }
            }
            UIPanelStats.Instance.LoadData();
        }

    }


    [Serializable]
    public class GameData
    {
        public int typeData;
        public int numberLevel;
        public int coins;
        public float timePlayed;
    }

    [Serializable]
    public class GameDataList
    {
        public List<GameData> dataList;
    }
}
