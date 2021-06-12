using UnityEngine;
using Newtonsoft.Json;

public class MAPlayerPrefData<T>
{
    public string KeyString { get; private set; }

    public T Data { get; private set; }

    public T DefaultData { get; private set; }

    public MAPlayerPrefData(string key, T defaultData) {
        KeyString = key;
        DefaultData = defaultData;
    }

    public void Init() {
        Data = LoadInternal();
    }

    public void Save(T data) {
        string jsonString = JsonConvert.SerializeObject(data);
        RawData = jsonString;

        Data = data;
    }

    private T LoadInternal()
    {
        string text = RawData;

        if (string.IsNullOrEmpty(text))
        {
            return DefaultData;
        }
        else
        {
            T aa = JsonConvert.DeserializeObject<T>(text);
            return aa;
        }
    }

    private string RawData
    {
        get
        {
            return PlayerPrefs.GetString(KeyString);
        }
        set
        {
            PlayerPrefs.SetString(KeyString, value);
            PlayerPrefs.Save();
        }
    }
}
