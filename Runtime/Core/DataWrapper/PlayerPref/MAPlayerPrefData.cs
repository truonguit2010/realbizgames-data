using UnityEngine;
using Newtonsoft.Json;

public class MAPlayerPrefData<T> : IPlayerPrefData<T>
{
    public string KeyString { get; private set; }
    public T Data { get; private set; }
    public T DefaultData { get; private set; }

    public MAPlayerPrefData(string key, T defaultData)
    {
        KeyString = key;
        DefaultData = defaultData;
    }

    // -------------------------------------------------
    // INIT
    // -------------------------------------------------
    public void Init()
    {
        Data = LoadInternal();
    }
    public void LazyInit() {
        if (Data == null) {
            Data = LoadInternal();
        }
    }
    private T LoadInternal()
    {
        string text = GetRawData();

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
    // -------------------------------------------------
    // WRITE
    // -------------------------------------------------
    public void Save(T data)
    {
        string jsonString = JsonConvert.SerializeObject(data);
        SaveRawData(jsonString);

        this.Data = data;
    }
    // -------------------------------------------------
    // READ
    // -------------------------------------------------
    public T Get() {
        LazyInit();
        return Data;
    }
    public string GetKey() {
        return KeyString;
    }
    // -------------------------------------------------
    // Utilities functions.
    // -------------------------------------------------
    public string GetRawData() {
        return PlayerPrefs.GetString(KeyString);
    }

    public void SaveRawData(string raw) {
        PlayerPrefs.SetString(KeyString, raw);
        PlayerPrefs.Save();
    }
}
