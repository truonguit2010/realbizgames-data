using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealbizGames.Data
{
    /// <summary>
    /// Nếu lấy từ Firebase Remote Config có thì lấy từ Remote Config.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultMasterDataMediator<T> : IMasterDataMediator<T>
    {

        private List<IMasterDataDictionary<T>> dataDics = new List<IMasterDataDictionary<T>>(); 

        public DefaultMasterDataMediator(string localFilePath, string firebaseRemoteKey) {
            dataDics = new List<IMasterDataDictionary<T>>();
            
            if (!string.IsNullOrEmpty(firebaseRemoteKey)) {
                FirebaseRemoteConfigLoader firebaseRemoteConfigLoader = new FirebaseRemoteConfigLoader(key: firebaseRemoteKey);
                string name = "FirebaseRemoteConfig";
                IMasterDataDictionary<T> dictionary = new GenericMasterDataDictionary<T>(jsonString: string.Empty, loader: firebaseRemoteConfigLoader, name: name);
                dataDics.Add(dictionary);
            }

            if (!string.IsNullOrEmpty(localFilePath)) {
                LocalFileLoader localFileLoader = new LocalFileLoader(filePath: localFilePath);
                string name = "LocalFileConfig";
                IMasterDataDictionary<T> dictionary = new GenericMasterDataDictionary<T>(jsonString: string.Empty, loader: localFileLoader, name: name);
                dataDics.Add(dictionary);
            }
        }

        // -------------------------------------------------
        // Chain
        // -------------------------------------------------
        public IMasterDataMediator<T> Add(IMasterDataDictionary<T> masterDataDictionary)
        {
            this.dataDics.Add(masterDataDictionary);
            return this;
        }

        // -------------------------------------------------
        // LOAD
        // -------------------------------------------------
        public void Load()
        {
            foreach (var item in dataDics)
            {
                item.Load();
            }
        }
        public void Reload()
        {
            foreach (var item in dataDics)
            {
                item.Reload();
            }
        }
        // -------------------------------------------------
        // READ
        // -------------------------------------------------
        public T FindById(string id)
        {
            foreach (var item in dataDics)
            {
                if (item.IsNotEmpty())
                {
                    return item.FindById(id);
                }
            }
            return default;
        }
        public List<T> FindAllByIds(List<string> ids)
        {
            foreach (var item in dataDics)
            {
                if (item.IsNotEmpty())
                {
                    return item.FindAllByIds(ids);
                }
            }
            return null;
        }
        public List<T> FindAll()
        {
            foreach (var item in dataDics)
            {
                if (item.IsNotEmpty())
                {
                    return item.FindAll();
                }
            }
            return null;
        }
        public List<string> GetAllKey() 
        {
            foreach (var item in dataDics)
            {
                if (item.IsNotEmpty())
                {
                    return item.GetAllKey();
                }
            }
            return null;
        }
    }
}