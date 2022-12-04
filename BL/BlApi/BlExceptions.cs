/// <summary>
/// BL exceptions module includes exceptions for BL layer. 
/// </summary>

namespace BlApi;

/// <summary>
///  BLNotExistException class- for Attempts to search, add or delete
/// an object that does not exist or with a missing identifier.
/// </summary>

public class BLNotExistException : Exception
{
    public BLNotExistException(DalApi.NotExistException inner ) : base("", inner) { }
    public override string Message => $"{InnerException.Message}";
                   
}



/// <summary>
/// BLAlreadyExistException class for Attempts to add an object.
/// </summary>


public class BLAlreadyExistException : Exception
{
    public BLAlreadyExistException(DalApi.AlreadyExistException inner): base("", inner) { }
    public override string Message => $"{InnerException.Message}";
           
}


/// <summary>
/// BLInValidInputException class for input errors.
/// </summary>
public class BLInValidInputException: Exception
{
    public override string Message => "Invalid input";
}


/// <summary>
/// BlOutOfStockException class - for Attempt to order an item that is out of stock, or amount not in stock.
/// </summary>
public class BlOutOfStockException : Exception
{
    public override string Message =>
                    "not enough in stock";

}

/// <summary>
/// exception for an invalid email format
/// </summary>
public class BlInvalidEmailException : Exception
{
    public override string Message =>
                    "invalid email exception";

}

/// <summary>
/// exception for a null value
/// </summary>
public class BlNullValueException : Exception
{
    public override string Message =>
                    "null value exception";

}


