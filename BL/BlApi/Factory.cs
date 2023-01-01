namespace BlApi;
using BlImplementation;
/// <summary>
/// Factory class creates BL instance.
/// </summary>
public static class Factory
{
    public static IBL Get() 
    { 
        BL bl= new BL();
        return bl;
    }
}
