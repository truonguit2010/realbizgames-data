using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAPlayerPrefDictionary<T> : IPlayerPrefDictionary<T>
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
    // -------------------------------------------------
    // INIT
    // -------------------------------------------------
    public void Init()
    {
        GenericPlayerPrefContainer.Init();
    }
    public void LazyInit() {
        GenericPlayerPrefContainer.LazyInit();
    }
    // -------------------------------------------------
    // WRITE
    // -------------------------------------------------
    public void Save(string key, T data)
    {
        LazyInit();
        Data[key] = data;
        GenericPlayerPrefContainer.Save(Data);
    }
    public void SaveAll(Dictionary<string, T> data)
    {
        GenericPlayerPrefContainer.Save(data);
    }
    public void MergeAndSave(Dictionary<string, T> data) {
        LazyInit();

        Dictionary<string, T> originalData = this.Data;
        foreach(KeyValuePair<string, T> entry in data) {
            originalData[entry.Key] = entry.Value;
        }
        GenericPlayerPrefContainer.Save(originalData);
    }
    // -------------------------------------------------
    // READ
    // -------------------------------------------------
    public T GetByKey(string key) {
        LazyInit();

        if (Data.ContainsKey(key)) {
            return Data[key];
        } else {
            return default;
        }
    }
    public List<T> GetAllByKeys(List<string> keys) {
        List<T> result = new List<T>();
        foreach (var key in keys)
        {
            var i = GetByKey(key);
            if (i != default) {
                result.Add(i);
            }
        }
        return result;
    }

    public List<T> GetAll() {
        LazyInit();
        return new List<T>(Data.Values);
    }

    public List<string> GetAllKey() {
        LazyInit();
        return new List<string>(Data.Keys);
    }

    

    

    

}
