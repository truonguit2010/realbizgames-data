using System.Collections.Generic;

public class GenericPlayerPrefDictionaryRepository<T> : IPlayerPrefDictionaryRepository<T>
{
    public string RecordKey { get; private set; }

    public MAPlayerPrefDictionary<T> MAPlayerPrefDictionary { get; private set; }

    public GenericPlayerPrefDictionaryRepository(string recordKey) {
        this.RecordKey = recordKey;
    }

    public void init()
    {
        lazyInit();
    }

    public void lazyInit()
    {
        if (MAPlayerPrefDictionary == null) {
            Dictionary<string, T> defaultData = new Dictionary<string, T>();
            MAPlayerPrefDictionary = new MAPlayerPrefDictionary<T>(RecordKey, defaultData);
            MAPlayerPrefDictionary.Init();
        }
    }

    public void refresh()
    {
        //
    }


    public T Get(string id)
    {
        lazyInit();
        return MAPlayerPrefDictionary.GetByKey(id);
    }

    public void Save(string id, T data)
    {
        lazyInit();
        MAPlayerPrefDictionary.Save(id, data);
    }

    public List<T> GetAll()
    {
        lazyInit();
        return MAPlayerPrefDictionary.GetAll();
    }

    public List<string> GetAllKey()
    {
        lazyInit();
        return MAPlayerPrefDictionary.GetAllKey();
    }
}
