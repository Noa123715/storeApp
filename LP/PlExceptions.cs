using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;

/// <summary>
/// exception for null value
/// </summary>
public class PlNullValueException : Exception
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public string nullField { get; set; }
    public PlNullValueException(string field)
    {
        nullField = field;
    }
    public override string Message => $@"{nullField} can not be null";

}

public class PlInvalidEmailException : Exception
{
    /// <summary>
    /// Default constructor.
    /// </summary>

    public override string Message => $@"invalid email exception";

}

public class PlGenericException : Exception
{

    public string err { get; set; }
    public PlGenericException(string err)
    {
        this.err = err;
    }
    public override string Message => "An unexpected system error occurred. try again.\r\nIf the problem recurs, send us a copy of the error to technical support: " + err;
}


