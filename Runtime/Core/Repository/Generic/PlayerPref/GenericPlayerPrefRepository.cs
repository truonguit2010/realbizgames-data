

public class GenericPlayerPrefRepository<T> : IPlayerPrefRepository<T>
{
    public MAPlayerPrefData<T> MAPlayerPrefData { get; private set; }

    public string RecordKey { get; private set; }

    public GenericPlayerPrefRepository(string recordKey) {
        RecordKey = recordKey;
    }

    public void init()
    {
        lazyInit();
    }

    public void lazyInit()
    {
        if (MAPlayerPrefData == null) {
            MAPlayerPrefData = new MAPlayerPrefData<T>(RecordKey, default);
            MAPlayerPrefData.Init();
        }
    }

    public void refresh()
    {
        //
    }

    public void Save(T data)
    {
        lazyInit();
        MAPlayerPrefData.Save(data);
    }

    public T GetT()
    {
        lazyInit();
        return MAPlayerPrefData.Data;
    }
}
