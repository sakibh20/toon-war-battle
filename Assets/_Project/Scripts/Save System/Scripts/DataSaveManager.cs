using System.IO;
using _Project.Core.Custom_Debug_Log.Scripts;
using Save_System.Scripts;
using UnityEngine;

namespace skb_sec._Project.Scripts.Save_System.Scripts
{
    public static class DataSaveManager 
    {
        private static DataSavingSettings DataSavingSettings;

        [RuntimeInitializeOnLoadMethod]
        private static void LoadDataSaveSettings()
        {
            DataSavingSettings = Resources.Load<DataSavingSettings>("Data Save Setting/Data Save Settings");
            if (!DataSavingSettings)
            {
                Debug.LogError("Data Save Settings Not Found, Please Create Settings / Check if The Path Is Correct");
            }
        }

        public static void SaveData(string data, string fileName)
        {
            var dataPath = CreateDataPath(fileName);
            CustomDebug.Log(dataPath);
            var result = DataEncryptor.Encrypt(data, DataSavingSettings.securityKey);
            File.WriteAllText(dataPath,result);
        }
        
        public static string GetData(string fileName)
        {
            var dataPath = CreateDataPath(fileName);

            if (CheckFile(dataPath))
            {
                var data = File.ReadAllText(dataPath);
                var result=DataDecryptor.Decrypt(data, DataSavingSettings.securityKey);
                return result;
            }

            return null;
        }


        public static void DeleteData(string fileName)
        {
            var dataPath = CreateDataPath(fileName);

            if (CheckFile(dataPath))
            {
               File.Delete(dataPath);
            }
        }
        
        private static string CreateDataPath(string fileName)
        {
            
            return Path.Combine(Application.persistentDataPath, fileName + ".txt");
        }

        private static bool CheckFile(string path)
        {
            return File.Exists(path);
        }
        
    }
}
