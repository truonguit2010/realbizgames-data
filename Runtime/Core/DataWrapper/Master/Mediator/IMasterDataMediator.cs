using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealbizGames.Data
{
    public interface IMasterDataMediator<T>
    {
        // -------------------------------------------------
        // Chain
        // -------------------------------------------------
        IMasterDataMediator<T> Add(IMasterDataDictionary<T> masterDataDictionary);
        // -------------------------------------------------
        // LOAD
        // -------------------------------------------------
        void Load();
        void Reload();
        // -------------------------------------------------
        // READ
        // -------------------------------------------------
        T FindById(string id);
        List<T> FindAllByIds(List<string> ids);
        List<T> FindAll();
        List<string> GetAllKey();
    }
}