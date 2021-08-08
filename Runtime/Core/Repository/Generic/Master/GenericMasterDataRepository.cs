using System.Collections.Generic;

public class GenericMasterDataRepository<T> : IMasterDataRepository<T>
{
    public RealbizGames.Data.IMasterDataDictionary<T> MasterDataDictionary;
    public string JsonFilePath { get; private set; }

    private IMasterDataDictionaryLoader loader;

    public GenericMasterDataRepository(string jsonFilePath) {
        JsonFilePath = jsonFilePath;

        loader = new RealbizGames.Data.LocalFileLoader(filePath: jsonFilePath);
    }

    public void init()
    {
        lazyInit();
    }

    public void lazyInit()
    {
        if (this.MasterDataDictionary == null) {
            MasterDataDictionary = new RealbizGames.Data.GenericMasterDataDictionary<T>();
            MasterDataDictionary.Load(loader: loader);
        }
    }

    public void refresh()
    {
        lazyInit();
    }

    public List<T> FindAllByIds(List<string> ids)
    {
        lazyInit();
        return MasterDataDictionary.FindAllByIds(ids: ids);
    }

    public T FindById(string id)
    {
        lazyInit();
        return MasterDataDictionary.FindById(id);
    }

    public List<T> FindAll()
    {
        lazyInit();
        return MasterDataDictionary.FindAll();
    }

    public List<string> GetAllKey() {
        lazyInit();
        return MasterDataDictionary.GetAllKey();
    }
}
