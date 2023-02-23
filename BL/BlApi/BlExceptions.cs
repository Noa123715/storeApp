/// <summary>
/// BL exceptions module includes exceptions for BL layer. 
/// </summary>

namespace BlApi;

/// <summary>
///  BlNotExistException class- for Attempts to search, add or delete
/// an object that does not exist or with a missing identifier.
/// </summary>

public class BlNotExistException : Exception
{
    public BlNotExistException(DalApi.NotExistException? inner = null) : base("", inner) { }
    public override string Message => $"{InnerException.Message}";

}



/// <summary>
/// BLAlreadyExistException class for Attempts to add an object.
/// </summary>


public class BlAlreadyExistException : Exception
{
    public BlAlreadyExistException(DalApi.AlreadyExistException? inner = null) : base("", inner) { }
    public override string Message => $"{InnerException.Message}";

}


/// <summary>
/// BLInValidInputException class for input errors.
/// </summary>
public class BlInValidInputException : Exception
{
    public override string Message => "Invalid input";
}
public class BlNegativeInputException : Exception
{
    public override string Message => "Negative value. enter only positive number.";
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


/// <summary>
/// exception for updating dates in wrong order
/// </summary>
public class BlWrongDateSequenceException : Exception
{
    public override string Message =>
                    "can't update dates in wrong sequence";

}


/// <summary>
/// exception for Illegal deletion attempt (for example: a product that has already been ordered ).
/// </summary>
public class BlIllegalDeletionAttempt : Exception
{
    public override string Message =>
                  "Illegal deletion attempt - the item exists in a confirmed order or does not exist at all.";

}