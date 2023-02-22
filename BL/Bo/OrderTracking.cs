

namespace BO;


public class OrderTracking
{
    public int ID { get; set; }
    public eOrderStatus Status { get; set; }
    public List<Tuple<DateTime?, string>> TrackList { get; set; } = new();

    //overriding the ToString function for printing the order-tracking's details
    public override string ToString()
    {
        string toString = $@"ID: {ID}, Status: {Status}, TrackList: ";
        foreach (var i in TrackList) { toString += " \n \t" + i.Item2 + " on " + i.Item1; };
        return toString;
    }
}


