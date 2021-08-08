
public interface IPlayerPrefRepository<T> : IRepository
{
    void Save(T data);

    T GetT();
}
