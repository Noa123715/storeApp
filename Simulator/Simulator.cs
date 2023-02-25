using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public static class Simulator
{
    private static BO.Order? CurrentOrder;
    private static Random? Random;
    private static IBL? bl = BlApi.Factory.Get();
    private static bool finish = true;

    public static event EventHandler Progress;
    public static event EventHandler StopSimulator;
    public static void Run()
    {
        Thread thread = new(Stop);
        thread.Start();
    }

    public static void Stop()
    {

    }
}