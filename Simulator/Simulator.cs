using BlApi;
using BlImplementation;
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
    private static IBL ?bl = BlApi.Factory.Get();
    //private static bool finish = true;
    private static bool stop { get; set; } = false;
    private static Random rand = new Random();
    public static event EventHandler? Progress;
    public static event EventHandler ?StopSimulator;
    private static SimulatorEventDetails Details { get; set; }
    public static event EventHandler UpdateProgress;
    private static int? orderId { get; set; }
    public static void Run()
    {
        new Thread(() =>
        {
            while (!stop) { 
            
                orderId = bl?.Order.ChooseOrder();
                if (orderId == 0)
                {
                    Stop();
                    break;
                }
                int time = rand.Next(5, 10);
                BO.Order order = bl.Order.ReadOrderProperties((int)orderId);
                Details = new SimulatorEventDetails(time, order);
                OnUpdateProgress();
                Thread.Sleep(1000 * Details.time);
                if (bl.Order.ReadOrderProperties((int)orderId).Status == BO.eOrderStatus.Ordered)
                    bl.Order.UpdateOrderSent((int)orderId);
                else
                    bl.Order.UpdateOrderDelivery((int)orderId);
            }
        }).Start();
    }




    private static void OnUpdateProgress()
    {
        if (UpdateProgress != null)
            UpdateProgress(null, Details);
    }




    public static void Stop()
    {
        if (StopSimulator != null)
            StopSimulator(null, EventArgs.Empty);
        stop = true;
    }
}