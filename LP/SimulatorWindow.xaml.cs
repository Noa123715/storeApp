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

    private BlApi.IBL Bl { get; set; }
    private bool isFinish = false;
    private static extern int SetWindow(IntPtr hWnd, int nIndex, int newLoad);
    private static extern int GetWindow(IntPtr hWnd, int nIndex);
    public SimulatorWindow(BlApi.IBL bl)
    {
        InitializeComponent();
        Bl = bl;
        backgroundWorker = new BackgroundWorker();
        backgroundWorker.DoWork += BackgroundWorker_DoWork;
        backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
        backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        backgroundWorker.WorkerReportsProgress = true;
        backgroundWorker.WorkerSupportsCancellation = true;
        backgroundWorker.RunWorkerAsync();
        DataContext = DateTime.Now.ToString();
    }

    private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (!isFinish)
        {
            isFinish = true;
            Simulator.Simulator.Stop();
        }
    }

    /// <summary>
    /// to cancel closing the window 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
    }

    /// <summary>
    /// By pressing the button the simulation will stop
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EndOfSimulator_Click(object sender, RoutedEventArgs e)
    {
        if (!isFinish)
        {
            isFinish = true;
            Simulator.Simulator.Stop();
        }
    }

    private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.Run();
        Simulator.Simulator.Progress += ChangeDisplay;
        Simulator.Simulator.StopSimulator += StopSimulator;
        if (!isFinish)
        {
            backgroundWorker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    private void StopSimulator(object? sender, EventArgs e)
    {
        MessageBox.Show(
            "simulator's update is finished",
            "simulator finish",
            MessageBoxButton.OK,
            MessageBoxImage.Information
            );
        Close();
    }

    private void ChangeDisplay(object? sender, EventArgs e)
    {

    }

    private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        NowLbl.DataContext = DateTime.Now.ToString();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        IntPtr hWnd = new WindowInteropHelper(this).Handle;
    }
}