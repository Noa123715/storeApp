using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

    public class SimulatorEventDetails: EventArgs
    {
    public int time { get; set; }
    public BO.Order? Order { get; set; }
    public SimulatorEventDetails(int _time, BO.Order? _order)
    {
        time = _time;
        Order = _order;
    }

}

