using System.Collections.Generic;

public interface IMasterDataRepository<T> : IRepository
{
    T FindById(string id);

    List<T> FindAllByIds(List<string> ids);
    
    List<T> FindAll();

    List<string> GetAllKey();

}
