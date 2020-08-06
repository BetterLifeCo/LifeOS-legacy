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

#endregion




// ReSharper disable NotAccessedVariable
// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable RedundantAssignment

namespace LifeOS.WPF
{
  using System;
  using System.IO;
  using Anotar.Serilog;
  using Common;
  using Common.Exceptions;
  using Common.Extensions;
  using Common.Services;
  using Common.Services.Configuration;
  using Common.Services.IO.Diagnostics;
  using SuperMemoAssistant.Services.Sentry;

  /// <summary>
  /// This class is initialized by Fody.ModuleInit when the module is loaded -- even before the WPF App is initialized
  /// </summary>
  public static class ModuleInitializer
  {
    #region Constants & Statics

    public static IDisposable SentryInstance { get; private set; }

    #endregion




    #region Methods

    public static void Initialize()
    {
      try
      {
        Svc.Configuration = new ConfigurationService(LOSFileSystem.ConfigDir);
        Svc.Logger        = LoggerFactory.Create(LOSConst.Name, Svc.Configuration, SentryEx.LogToSentry);

        Logger.ReloadAnotarLogger(typeof(ModuleInitializer));

        // ReSharper disable once RedundantNameQualifier
        var appType    = typeof(App);
        var losVersion = appType.GetAssemblyVersion();

        var releaseName = $"LifeOS@{losVersion}";

        SentryInstance = SentryEx.Initialize("https://b9b0d5b93d044c15a98915cb7f3b2c66@o218793.ingest.sentry.io/5379722", releaseName);
      }
      catch (LOSException ex)
      {
        LogTo.Warning(ex, "Error during SuperMemoAssistant Initialize().");
        File.WriteAllText(LOSFileSystem.TempErrorLog.FullPath, $"Error during SuperMemoAssistant Initialize(): {ex}");
      }
      catch (Exception ex)
      {
        LogTo.Error(ex, "Exception thrown during SuperMemoAssistant module Initialize().");
        File.WriteAllText(LOSFileSystem.TempErrorLog.FullPath, $"Exception thrown during SuperMemoAssistant Initialize(): {ex}");
      }
    }

    #endregion
  }
}
