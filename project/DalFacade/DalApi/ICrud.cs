
namespace DalApi;
//hfkggggggg
public interface Icrud<T>
{
    public T Create(T item);
    public T Read(int id);
    public IEnumerable<T> ReadAll();
    public void Delete(int id);
}

