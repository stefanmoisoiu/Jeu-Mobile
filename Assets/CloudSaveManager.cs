using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager Instance { get; private set; }
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

    public static async void SaveData(SavedValue savedValue)
    {
        var data = new Dictionary<string, object> { { savedValue.name, savedValue.value } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    }

    public static async Task<SavedValue> GetSaveData(string name, object valueIfNull)
    {
        try
        {
            var saveData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { name });
            return new SavedValue(name, saveData[name]);
        }
        catch
        {
            SavedValue newData = new SavedValue(name, valueIfNull);
            SaveData(newData);
            return newData;
        }
    }

    [System.Serializable]
    public struct SavedValue
    {
        public string name;
        public object value;
        public SavedValue(string key, object value)
        {
            this.name = key;
            this.value = value;
        }
    }

}