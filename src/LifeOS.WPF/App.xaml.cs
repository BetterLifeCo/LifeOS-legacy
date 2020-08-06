namespace LifeOS.WPF
{
  using Common.Services;
  using System.Windows;
  using System.Windows.Threading;

  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      //
      // Log Dispatcher unhandled exceptions
      Application.Current.DispatcherUnhandledException += LogDispatcherUnhandledException; ;

      base.OnStartup(e);
    }

    private void LogDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      Svc.Logger.LogUnhandledException(e.Exception, e.Handled);
    }
  }
}
