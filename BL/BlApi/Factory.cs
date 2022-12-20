namespace BlApi;
using BlImplementation;

public static class Factory
{
    public static IBL Get() 
    { 
        BL bl= new BL();
        return bl;
    }
}
