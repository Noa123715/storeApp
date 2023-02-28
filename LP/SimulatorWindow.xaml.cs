using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Simulator;
using System.Diagnostics;

namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    Duration Duration;
    DoubleAnimation animation;
    ProgressBar progressBar;
    BackgroundWorker backgroundWorker;
    public BO.eOrderStatus nextStatus;
    private BlApi.IBL Bl { get; set; }
    private bool isFinish = false;
    Tuple<BO.Order, int> DetailsTuple;
    ProgressBar ProgressingOrderBar;
    Duration duration;
    DoubleAnimation doubleanimation;
    private Stopwatch stopWatch;
    //private static extern int SetWindow(IntPtr hWnd, int nIndex, int newLoad);
    //private static extern int GetWindow(IntPtr hWnd, int nIndex);
    public SimulatorWindow(BlApi.IBL bl)
    {
        InitializeComponent();
        Bl = bl;
        stopWatch = new Stopwatch();
        backgroundWorker = new BackgroundWorker();
        backgroundWorker.DoWork += BackgroundWorker_DoWork;
        backgroundWorker.ProgressChanged += Worker_ProgressChanged;
        backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        backgroundWorker.WorkerReportsProgress = true;
        backgroundWorker.WorkerSupportsCancellation = true;
        backgroundWorker.RunWorkerAsync();
        DataContext = DetailsTuple;
        stopWatch.Restart();
        Simulator.Simulator.StopSimulator += StopSimulator;
        Simulator.Simulator.UpdateProgress += BackgroundWorker_ProgressChanged;

    }

    private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.Simulator.StopSimulator -= StopSimulator;
        Simulator.Simulator.UpdateProgress -= BackgroundWorker_ProgressChanged;
        MessageBox.Show("simulation stoped");
        isFinish= true;
        this.Close();
    }

    /// <summary>
    /// to cancel closing the window 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if(!isFinish)
            e.Cancel = true;
    }

    /// <summary>
    /// By pressing the button the simulation will stop
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EndOfSimulator_Click(object sender, RoutedEventArgs e)
    {
      
        if (backgroundWorker.WorkerSupportsCancellation == true)
            backgroundWorker.WorkerSupportsCancellation = false;
       

    }

    private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.Run();
  
        while (backgroundWorker.WorkerSupportsCancellation)
        {
            backgroundWorker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    private void StopSimulator(object? sender, EventArgs e)
    {
        MessageBox.Show("There are no more orders to process");
        EndOfSimulator_Click(sender, e as RoutedEventArgs);
    }


    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        timerTextBlock.Content = timerText;
    }

    private void BackgroundWorker_ProgressChanged(object? sender, EventArgs e)
    {
     

            if (e is not SimulatorEventDetails)
                return;
            SimulatorEventDetails details = e as SimulatorEventDetails;
            DetailsTuple = new Tuple<BO.Order, int>(details.Order, details.time);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(BackgroundWorker_ProgressChanged, sender, e);
            }
            else
            {
                nextStatus = (BO.eOrderStatus)((int)details.Order.Status + 1);
                nextStateTxt.Text = nextStatus.ToString();
                DataContext = DetailsTuple;

                ProgressBarStart(details.time);
            }
            
    }

    void ProgressBarStart(int sec)
    {
        if (ProgressingOrderBar != null)
        {
            SBar.Items.Remove(ProgressingOrderBar);
        }
        ProgressingOrderBar = new ProgressBar();
        ProgressingOrderBar.IsIndeterminate = false;
        ProgressingOrderBar.Orientation = Orientation.Horizontal;
        ProgressingOrderBar.Width = 500;
        ProgressingOrderBar.Height = 30;
        duration = new Duration(TimeSpan.FromSeconds(sec * 2));
        doubleanimation = new DoubleAnimation(200.0, duration);
        ProgressingOrderBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        SBar.Items.Add(ProgressingOrderBar);
    }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        IntPtr hWnd = new WindowInteropHelper(this).Handle;
    }
}