using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerPrefDictionary<T>
{
    // -------------------------------------------------
    // INIT
    // -------------------------------------------------
    void Init();
    void LazyInit();
    // -------------------------------------------------
    // WRITE
    // -------------------------------------------------
    void Save(string key, T data);
    void SaveAll(Dictionary<string, T> data);
    void MergeAndSave(Dictionary<string, T> data);
    // -------------------------------------------------
    // READ
    // -------------------------------------------------
    T GetByKey(string key);
    List<T> GetAllByKeys(List<string> keys);
    List<T> GetAll();
    List<string> GetAllKey();
}
