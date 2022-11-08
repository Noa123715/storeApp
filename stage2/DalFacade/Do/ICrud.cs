/// <summary>
/// 
/// </summary>

namespace Dal.DO;

public interface ICrud<T> where T : struct
{
    void Create(T item);
    T ReadById(int id);
    T[] ReadAll();
    void Update(T item, int id, int option);
    void Delete(int id);
}

