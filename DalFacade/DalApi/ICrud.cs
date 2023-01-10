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
   


    /// <summary>
    /// read all IEnumerable of product, order, or orderItem According to a certain condition or the whole - by default
    /// </summary>
    /// <param name="condition"></param>
    /// <returns> the required Ienumerable </returns>
    public IEnumerable<T> ReadAll(Func<T,bool>? condition= null);

    public T Read(Func<T, bool> condition);
    public void UpDate(T item);
    public void Delete(int id);
}

