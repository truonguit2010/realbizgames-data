using System.Collections.Generic;
using Newtonsoft.Json;

namespace RealbizGames.Data
{
    public class GenericMasterDataDictionary<T> : IMasterDataDictionary<T>
    {
        public Dictionary<string, T> dataMap { get; private set; }

        public string jsonString { get; private set; }

        public GenericMasterDataDictionary(string jsonString = "")
        {
            this.jsonString = jsonString;
            Init();
        }

        public void Load(IMasterDataDictionaryLoader loader)
        {
            jsonString = loader.load();
            Init();
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

        private void Init()
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
    }

}
