using System.Collections.Generic;

public interface IPlayerPrefDictionaryRepository<T> : IRepository
{
    void Save(string id, T data);

    T Get(string id);

    List<T> GetAll();

    List<string> GetAllKey();
}
