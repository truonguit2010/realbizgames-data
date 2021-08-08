using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class MAMasterDataDictionary<T>
{
    public string path {
        get;
        private set;
    }

    public Dictionary<string, T> dataMap { get; private set; }

    public MAMasterDataDictionary(string jsonPath) {
        this.path = jsonPath;
    }

    public void Init() {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        dataMap = JsonConvert.DeserializeObject<Dictionary<string, T>>(textAsset.text);
    }

    public T FindById(string id) {
        if (dataMap.ContainsKey(id)) {
            return dataMap[id];
        } else {
            return default;
        }
    }
}
