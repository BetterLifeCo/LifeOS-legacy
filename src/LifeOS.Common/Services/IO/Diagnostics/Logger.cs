#region License & Metadata

// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// 
// 
// Created On:   2020/03/29 00:21
// Modified On:  2020/04/07 06:55
// Modified By:  Alexis

#endregion




namespace LifeOS.Common.Services.IO.Diagnostics
{
  using System;
  using System.Reflection;
  using System.Runtime.ExceptionServices;
  using System.Threading.Tasks;
  using Anotar.Serilog;
  using Configuration;
  using Extensions;
  using Serilog;
  using Serilog.Core;
  using Serilog.Events;

  /// <summary>Contains logic for the Logger</summary>
  public sealed class Logger
  {
    #region Properties & Fields - Non-Public

    private LoggingLevelSwitch LevelSwitch                { get; }
    private bool               IsLogFirstChangeRegistered { get; set; }

    #endregion




    #region Constructors

    /// <summary>Instantiates a new logger</summary>
    /// <param name="config">The logger's config</param>
    /// <param name="levelSwitch">The logging level switcher</param>
    internal Logger(LoggerCfg config, LoggingLevelSwitch levelSwitch)
    {
      Config      = config;
      LevelSwitch = levelSwitch;

      RegisterExceptionLoggers();

      LogTo.Debug("Logger initialized");
    }

    #endregion




    #region Properties & Fields - Public

    /// <summary>The config for the current logger</summary>
    public LoggerCfg Config { get; private set; }

    #endregion




    #region Methods

    /// <summary>Shutdowns the current logger and flush out the pending content to file</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public void Shutdown()
    {
      try
      {
        Log.CloseAndFlush();
      }
      catch
      {
        /* Ignore */
      }
    }

    /// <summary>Update logger settings with new <see cref="Config" /> parameters.</summary>
    public void ReloadConfig()
    {
      SetMinimumLevel(Config.LogLevel);

      if (Config.LogFirstChanceExceptions)
        RegisterFirstChanceExceptionLogger();

      else
        UnregisterFirstChanceExceptionLogger();
    }

    internal async Task ReloadConfigFromFileAsync(ConfigurationServiceBase cfgService)
    {
      Config = await cfgService.LoadAsync<LoggerCfg>().ConfigureAwait(false);

      ReloadConfig();
    }

    /// <summary>Changes the minimum logging level written to file</summary>
    /// <param name="level">The new minimum logging level</param>
    /// <returns></returns>
    public LogEventLevel SetMinimumLevel(LogEventLevel level)
    {
      var oldLevel = LevelSwitch.MinimumLevel;
      LevelSwitch.MinimumLevel = level;

      LogTo.Information("Logging level changed from {V} to {V1}", oldLevel.Name(), level.Name());

      return oldLevel;
    }

    private void RegisterExceptionLoggers()
    {
      AppDomain.CurrentDomain.UnhandledException += LogUnhandledException;

      if (Config.LogFirstChanceExceptions)
        RegisterFirstChanceExceptionLogger();

      LogTo.Debug("Exception loggers registered (first-chance logging: {V})", Config.LogFirstChanceExceptions ? "on" : "off");
    }

    private void RegisterFirstChanceExceptionLogger()
    {
      if (IsLogFirstChangeRegistered)
        return;

      IsLogFirstChangeRegistered                   =  true;
      AppDomain.CurrentDomain.FirstChanceException += LogFirstChanceException;
    }

    private void UnregisterFirstChanceExceptionLogger()
    {
      if (IsLogFirstChangeRegistered == false)
        return;

      IsLogFirstChangeRegistered                   =  false;
      AppDomain.CurrentDomain.FirstChanceException -= LogFirstChanceException;
    }

    public void LogUnhandledException(Exception ex, bool handled = false)
    {
      LogTo.Fatal(ex, "Unhandled exception");

      if (handled == false)
        Shutdown();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "Serilog004:Constant MessageTemplate verifier", Justification = "<Pending>")]
    private void LogUnhandledException(object _, UnhandledExceptionEventArgs e)
    {
      if (e.ExceptionObject is Exception ex)
      {
        var msg = "Unhandled exception" + (e.IsTerminating ? ", terminating" : string.Empty);

        if (e.IsTerminating)
          LogTo.Fatal(ex, msg);

        else
          LogTo.Error(ex, msg);
      }

      if (e.IsTerminating)
        Shutdown();
    }

    private void LogFirstChanceException(object _, FirstChanceExceptionEventArgs e)
    {
      LogTo.Error(e.Exception, "First chance exception");
    }

    /// <summary>See https://github.com/Fody/Anotar/issues/114</summary>
    /// <typeparam name="T"></typeparam>
    public static void ReloadAnotarLogger<T>()
    {
      ReloadAnotarLogger(typeof(T));
    }

    /// <summary>See https://github.com/Fody/Anotar/issues/114</summary>
    /// <param name="classType"></param>
    public static void ReloadAnotarLogger(Type classType)
    {
      FieldInfo field;

      if ((field = classType.GetField("AnotarLogger", BindingFlags.NonPublic | BindingFlags.Static)) != null)
      {
        var logger = Log.ForContext(classType);
        field.SetValue(null, logger);
      }
    }

    #endregion
  }
}
