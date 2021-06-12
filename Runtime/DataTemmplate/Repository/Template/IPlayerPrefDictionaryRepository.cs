
public interface IPlayerPrefDictionaryRepository<T> : IRepository
{
    void Save(string id, T data);

    T Get(string id);
}
