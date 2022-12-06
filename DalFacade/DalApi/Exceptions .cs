namespace DalApi;
/// <summary>
///  exeptions for DalFacade, inherit from  Exception
/// </summary>


/// <summary>
///  NotExistException class- for Attempts to search, add or delete
/// an object that does not exist or with a missing identifier.
/// </summary>
public class NotExistException: Exception
{
     public override string Message => 
        "Error - the object does not exist or missing ID";
}

/// <summary>
/// AlreadyExistException class for Attempts to add an object.
/// </summary>
public class AlreadyExistException: Exception
{
    public override string Message =>
            "Error - ID already exists";
}
/// <summary>
/// 
/// </summary>
public class NonValidNumberException: Exception
{
    public override string Message =>
            "You entered a none valid number";
}