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
// Modified On:  2020/04/06 19:46
// Modified By:  Alexis

#endregion


namespace LifeOS.Common
{
  using System;
  using System.IO;
  using global::Extensions.System.IO;

  /// <summary>Defines constants to access the various core files of LOS</summary>
  public static class LOSFileSystem
  {
    #region Constants & Statics

    /// <summary>The config folder name for sma- and collection-specific configuration files</summary>
    public const string ConfigsFolder = "configs";

    /// <summary>LOS's root folder under %LocalAppData%</summary>
    public static DirectoryPath AppRootDir =>
      Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        LOSConst.Name
      );

    /// <summary>LOS's data folder (under user profile) TODO: Make this configurable</summary>
    public static DirectoryPath AppDataRootDir =>
      Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        LOSConst.Name
      );

    /// <summary>LOS's log folder</summary>
    public static DirectoryPath LogDir => AppDataRootDir.Combine("Logs");

    /// <summary>LOS's configs folder</summary>
    public static DirectoryPath ConfigDir => AppDataRootDir.Combine("Configs");

    /// <summary>LOS's data folder</summary>
    public static DirectoryPath DataDir => AppDataRootDir.Combine("Data");

    /// <summary>Path to Update.exe</summary>
    public static FilePath UpdaterExeFile => AppRootDir.CombineFile(LOSConst.Assembly.Updater);

    public static FilePath TempErrorLog => Path.Combine(Path.GetTempPath(), "SuperMemoAssistant.log");

    #endregion
  }
}
