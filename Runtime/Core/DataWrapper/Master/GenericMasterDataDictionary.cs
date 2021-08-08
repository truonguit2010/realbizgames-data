using System.Collections.Generic;
using Newtonsoft.Json;

namespace RealbizGames.Data
{
    public class GenericMasterDataDictionary<T> : IMasterDataDictionary<T>
    {
        public Dictionary<string, T> dataMap { get; private set; }

        public string jsonString { get; private set; }

        private IMasterDataDictionaryLoader _loader;
        private string _name;

        public GenericMasterDataDictionary(string jsonString = "", IMasterDataDictionaryLoader loader = null, string name = null)
        {
            this.jsonString = jsonString;
            this._loader = loader;
            this._name = name;
            LoadInternal();
        }
        // -------------------------------------------------
        // INIT
        // -------------------------------------------------
        public void Load() {
            jsonString = this._loader.load();
            LoadInternal();
        }
        public void Load(IMasterDataDictionaryLoader loader)
        {
            this._loader = loader;
            jsonString = loader.load();
            LoadInternal();
        }
        public void Merge(IMasterDataDictionaryLoader loader)
        {
            string more = loader.load();
            Dictionary<string, T> moreDict = json2Dic(more);

            foreach (KeyValuePair<string, T> entry in moreDict)
            {
                dataMap[entry.Key] = entry.Value;
            }
        }
        public void Reload() {
            jsonString = this._loader.load();
            LoadInternal();
        }
        private void LoadInternal()
        {
            if (!string.IsNullOrEmpty(jsonString))
            {
                dataMap = json2Dic(jsonString: jsonString);
            }
            else
            {
                dataMap = new Dictionary<string, T>();
            }
        }
        private Dictionary<string, T> json2Dic(string jsonString)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonString);
        }
        // -------------------------------------------------
        // VALIDATING
        // -------------------------------------------------
        public bool IsNotEmpty() {
            return dataMap.Count > 0;
        }
        public bool IsEmpty() {
            return dataMap.Count == 0;
        }
        // -------------------------------------------------
        // READ
        // -------------------------------------------------
        public string GetName() {
            return _name;
        }
        public T FindById(string id)
        {
            if (dataMap.ContainsKey(id))
            {
                return dataMap[id];
            }
            else
            {
                return default;
            }
        }

        public List<T> FindAll()
        {
            return new List<T>(dataMap.Values);
        }


        public List<T> FindAllByIds(List<string> ids)
        {
            List<T> valuatedData = new List<T>();

            foreach (string id in ids)
            {
                if (dataMap.ContainsKey(id))
                {
                    valuatedData.Add(dataMap[id]);
                }
            }
            return valuatedData;

        }
        public List<string> GetAllKey() {
            return new List<string>(dataMap.Keys);
        }
                
    }

}
