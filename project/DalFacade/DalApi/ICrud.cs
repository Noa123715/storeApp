
namespace DalApi;
public interface ICrud<T>
{
    public T Create(T item);
    public T Read(int id);
    public IEnumerable<T> ReadAll();
    public void UpDate(T item);
    public void Delete(int id);
}

