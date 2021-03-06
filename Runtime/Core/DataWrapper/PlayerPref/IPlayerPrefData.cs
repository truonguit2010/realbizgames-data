
public interface IPlayerPrefData<T>
{
    // -------------------------------------------------
    // INIT
    // -------------------------------------------------
    void Init();
    void LazyInit();
    // -------------------------------------------------
    // WRITE
    // -------------------------------------------------
    void Save(T data);
    // -------------------------------------------------
    // READ
    // -------------------------------------------------
    T Get();
    string GetKey();
    // -------------------------------------------------
    // Utilities functions.
    // -------------------------------------------------
    string GetRawData();
    void SaveRawData(string raw);
}
