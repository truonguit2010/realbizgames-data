using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAPlayerPrefDictionary<T>
{
    public Dictionary<string, T> Data
    {
        get
        {
            return GenericPlayerPrefContainer.Data;
        }
    }

    public MAPlayerPrefData<Dictionary<string, T>> GenericPlayerPrefContainer { get; private set; }

    public MAPlayerPrefDictionary(string key, Dictionary<string, T> defaultData)
    {
        GenericPlayerPrefContainer = new MAPlayerPrefData<Dictionary<string, T>>(key, defaultData);
    }

    public void Init()
    {
        GenericPlayerPrefContainer.Init();
    }

    public T GetByKey(string key) {
        if (Data.ContainsKey(key)) {
            return Data[key];
        } else {
            return default;
        }
    }

    public List<T> GetAll() {
        return new List<T>(Data.Values);
    }

    public List<string> GetAllKey() {
        return new List<string>(Data.Keys);
    }

    public void Save(string key, T data)
    {
        Data[key] = data;
        GenericPlayerPrefContainer.Save(Data);
    }

    public void SaveAll(Dictionary<string, T> data)
    {
        GenericPlayerPrefContainer.Save(data);
    }

    public void MergeAndSave(Dictionary<string, T> data) {
        Dictionary<string, T> originalData = this.Data;

        foreach(KeyValuePair<string, T> entry in data) {
            originalData[entry.Key] = entry.Value;
        }

        GenericPlayerPrefContainer.Save(originalData);
    }

}
