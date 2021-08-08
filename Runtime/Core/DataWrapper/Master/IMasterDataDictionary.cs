using System.Collections.Generic;

namespace RealbizGames.Data
{
    public interface IMasterDataDictionary<T>
    {
        // -------------------------------------------------
        // INIT
        // -------------------------------------------------
        void Load();
        void Load(IMasterDataDictionaryLoader loader);
        void Merge(IMasterDataDictionaryLoader loader);
        void Reload();
        // -------------------------------------------------
        // VALIDATING
        // -------------------------------------------------
        bool IsNotEmpty();
        bool IsEmpty();
        // -------------------------------------------------
        // READ
        // -------------------------------------------------
        string GetName();
        T FindById(string id);
        List<T> FindAllByIds(List<string> ids);
        List<T> FindAll();
        List<string> GetAllKey();

    }

}
