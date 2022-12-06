
namespace DalApi;
/// <summary>
/// Icrud- interface for crud methods:
/// create <see cref="Create"/>
/// read <see cref="Read"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrud<T>
{
    /// <summary>
    /// create a new Item from 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(T item);
    public T Read(int id);
    public IEnumerable<T> ReadAll();
    public void UpDate(T item);
    public void Delete(int id);
}

