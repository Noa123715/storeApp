namespace DalApi;
/// <summary>
///  exeptions for DalFacade, inherit from  Exception
/// </summary>


// NotExistException class- for Attempts to search, add or delete
// an object that does not exist or with a missing identifier.
public class NotExistException: Exception
{
     public override string Message => 
        "Error - the object does not exist or missing ID";
}

// AlreadyExistException class for Attempts to add an object.

public class AlreadyExistException: Exception
{
    public override string Message =>
            "Error - ID already exists";
}