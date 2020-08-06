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
// Modified On:  2020/04/07 06:43
// Modified By:  Alexis

#endregion




namespace LifeOS.Common.Services.IO.Diagnostics
{
  using System;
  using Anotar.Serilog;
  using Configuration;
  using global::Extensions.System.IO;
  using Serilog;
  using Serilog.Core;
  using Serilog.Exceptions;

  /// <summary>
  /// Factory to instantiate logger instances.
  /// This is separate from Logger due to race condition with Fody's Anotar.Serilog
  /// See https://github.com/Fody/Anotar/issues/114
  /// </summary>
  public static class LoggerFactory
  {
    #region Constants & Statics

    private const string OutputFormat = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    #endregion




    #region Methods

    /// <summary>Instantiates a new Serilog logger</summary>
    /// <param name="appName">The app name used in the file name</param>
    /// <param name="loggerCfg">Optional config</param>
    /// <param name="levelSwitch">The minimum logging level switch</param>
    /// <param name="configPredicate">A predicate to configure to the logger</param>
    /// <returns>New Serilog logger instance</returns>
    public static ILogger CreateSerilog(
      string                appName,
      LoggerCfg             loggerCfg       = null,
      LoggingLevelSwitch    levelSwitch     = null,
      LoggerConfigPredicate configPredicate = null)
    {
      var loggerConfig = new LoggerConfiguration()
                         .Enrich.WithExceptionDetails()
                         .WriteTo.Debug(outputTemplate: OutputFormat)
                         .WriteTo.Async(
                           a =>
                             a.RollingFile(
                               GetLogFilePath(appName).FullPath,
                               fileSizeLimitBytes: Math.Max(loggerCfg?.LogMaxSize ?? 5242880, 10485760),
                               retainedFileCountLimit: 7,
                               shared: true,
                               outputTemplate: OutputFormat
                             ));
      //.WriteTo.File(
      //  GetLogFilePath(appName).FullPath,
      //  outputTemplate: OutputFormat);
      //.WriteTo.RollingFile(
      //  GetLogFilePath(appName).FullPath,
      //  fileSizeLimitBytes: 5242880,
      //  retainedFileCountLimit: 7,
      //  shared: false,
      //  outputTemplate: OutputFormat
      //);

      if (levelSwitch != null)
        loggerConfig = loggerConfig.MinimumLevel.ControlledBy(levelSwitch);

      if (configPredicate != null)
        loggerConfig = configPredicate(loggerConfig);

      return loggerConfig.CreateLogger();
    }

    /// <summary>Create a new SMA logger instance</summary>
    /// <param name="appName">The app name used in the file name</param>
    /// <param name="sharedConfig">The shared config service to load the logging configuration file</param>
    /// <param name="configPredicate">A predicate to configure to the logger</param>
    /// <returns>New logger instance</returns>
    public static Logger Create(
      string                   appName,
      ConfigurationServiceBase sharedConfig,
      LoggerConfigPredicate    configPredicate = null)
    {
      if (Svc.Logger != null)
        throw new NotSupportedException();

      var config      = LoadConfig(sharedConfig);
      var levelSwitch = new LoggingLevelSwitch(config.LogLevel);

      Log.Logger = CreateSerilog(appName, config, levelSwitch, configPredicate);

      return new Logger(config, levelSwitch);
    }

    /// <summary>Loads the logging configuration</summary>
    /// <param name="sharedConfig">The shared configuration service</param>
    /// <returns></returns>
    public static LoggerCfg LoadConfig(ConfigurationServiceBase sharedConfig)
    {
      try
      {
        return sharedConfig.Load<LoggerCfg>() ?? new LoggerCfg();
      }
      catch (Exception ex)
      {
        LogTo.Error(ex, "Exception while loading logger config");

        return new LoggerCfg();
      }
    }

    private static FilePath GetLogFilePath(string appName, bool withDate = true)
    {
      var logDir = LOSFileSystem.LogDir;

      if (logDir.Exists() == false)
        logDir.Create();

      return withDate ? logDir.CombineFile($"{appName}-{{Date}}.log") : logDir.CombineFile($"{appName}.log");
    }

    #endregion




    /// <summary>Customizes the logger configuration</summary>
    /// <param name="config">The logger configuration</param>
    /// <returns>The modified configuration object</returns>
    public delegate LoggerConfiguration LoggerConfigPredicate(LoggerConfiguration config);
  }
}
