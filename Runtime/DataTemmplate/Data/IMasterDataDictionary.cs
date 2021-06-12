using System.Collections.Generic;

namespace RealbizGames.Data
{
    public interface IMasterDataDictionary<T>
    {
        void Load(IMasterDataDictionaryLoader loader);

        void Merge(IMasterDataDictionaryLoader loader);

        T FindById(string id);

        List<T> FindAllByIds(List<string> ids);

        List<T> FindAll();
    }

}
