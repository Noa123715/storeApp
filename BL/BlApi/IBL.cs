/// <summary>
/// BL interface
/// </summary>

namespace BlApi;
public interface IBL
{
    public ICart Cart { get; }
    public IOrder Order { get; }
    public IProduct Product { get; }
}
