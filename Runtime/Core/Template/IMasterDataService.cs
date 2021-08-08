using System.Collections.Generic;

public interface IMasterDataService<T> : IService
{
    T Get(string id);

    List<T> GetAll();
}
