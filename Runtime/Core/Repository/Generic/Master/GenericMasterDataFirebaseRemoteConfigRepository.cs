using System.Collections.Generic;

namespace RealbizGames.Data
{
    public class GenericMasterDataFirebaseRemoteConfigRepository<T> : IMasterDataRepository<T>
    {

        public string key { get; private set; }

        public IMasterDataDictionary<T> MasterDataDictionary;

        private IMasterDataDictionaryLoader loader;

        public GenericMasterDataFirebaseRemoteConfigRepository(string key)
        {
            this.key = key;
            loader = new FirebaseRemoteConfigLoader(key: key);
        }

        public List<T> FindAll()
        {
            lazyInit();
            return MasterDataDictionary.FindAll();
        }

        public List<T> FindAllByIds(List<string> ids)
        {
            lazyInit();
            return MasterDataDictionary.FindAllByIds(ids: ids);
        }

        public T FindById(string id)
        {
            lazyInit();
            return MasterDataDictionary.FindById(id: id);
        }

        public List<string> GetAllKey() {
            lazyInit();
            return MasterDataDictionary.GetAllKey();
        }

        public void init()
        {
            lazyInit();
        }

        public void lazyInit()
        {
            if (this.MasterDataDictionary == null) {
                this.MasterDataDictionary = new GenericMasterDataDictionary<T>(jsonString: string.Empty);
                this.MasterDataDictionary.Load(loader: loader);
            }
        }

        public void refresh()
        {
            lazyInit();
        }
    }
}