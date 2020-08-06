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
// Modified On:  2020/04/06 19:27
// Modified By:  Alexis

#endregion




namespace LifeOS.Common.Services.Configuration
{
  using System;
  using System.IO;
  using System.Threading.Tasks;
  using Anotar.Serilog;
  using Exceptions;
  using global::Extensions.System.IO;
  using Newtonsoft.Json;

  /// <inheritdoc/>
  public class ConfigurationService : ConfigurationServiceBase
  {
    #region Properties & Fields - Non-Public

    private readonly DirectoryPath _dirPath;

    #endregion




    #region Constructors

    /// <inheritdoc />
    public ConfigurationService(DirectoryPath dirPath)
    {
      _dirPath = dirPath;

      EnsureFolderExists();
    }

    #endregion




    #region Methods Impl

    /// <inheritdoc />
    protected override DirectoryPath GetDefaultConfigDirectoryPath()
    {
      return _dirPath;
    }

    #endregion
  }

  
  /// <summary>
  /// Facilitates loading and saving configuration file for SMA plugins
  /// </summary>
  public abstract class ConfigurationServiceBase
  {
    #region Methods

    /// <summary>
    ///   Loads the configuration for type <typeparamref name="T" />. The configuration file's name is generated based on the
    ///   type's name
    /// </summary>
    /// <param name="dirPath">Optional folder in which to look for the configuration file</param>
    /// <typeparam name="T">The configuration model</typeparam>
    /// <returns>Config object instance or null</returns>
    /// <exception cref="LOSException">When the configuration file couldn't be loaded</exception>
    public async Task<T> LoadAsync<T>(DirectoryPath dirPath = null)
    {
      dirPath ??= GetDefaultConfigDirectoryPath();

      try
      {
        using var stream = OpenConf(dirPath.FullPath, typeof(T), FileAccess.Read);
        using var reader = new StreamReader(stream);

        return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync().ConfigureAwait(false));
      }
      catch (Exception ex)
      {
        var filePath = GetConfigFilePath(dirPath, typeof(T));
        LogTo.Warning(ex, "Failed to load config {FilePath}", filePath);

        throw new LOSException($"Failed to load config {filePath}", ex);
      }
    }

    /// <summary>
    ///   Loads the configuration for type <typeparamref name="T" />. The configuration file's name is generated based on the
    ///   type's name
    /// </summary>
    /// <param name="dirPath">Optional folder in which to look for the configuration file</param>
    /// <typeparam name="T">The configuration model</typeparam>
    /// <returns>Config object instance or null</returns>
    /// <exception cref="LOSException">When the configuration file couldn't be loaded</exception>
    public T Load<T>(DirectoryPath dirPath = null)
    {
      dirPath ??= GetDefaultConfigDirectoryPath();

      try
      {
        using var stream = OpenConf(dirPath.FullPath, typeof(T), FileAccess.Read);
        using var reader = new StreamReader(stream);

        return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
      }
      catch (Exception ex)
      {
        var filePath = GetConfigFilePath(dirPath, typeof(T));
        LogTo.Warning(ex, "Failed to load config {FilePath}", filePath);

        throw new LOSException($"Failed to load config {filePath}", ex);
      }
    }

    /// <summary>Save the <paramref name="config" /> instance to file</summary>
    /// <param name="config">The config instance</param>
    /// <param name="dirPath">Optional folder in which to save the configuration file</param>
    /// <typeparam name="T">The configuration model</typeparam>
    /// <returns>Success of operation</returns>
    public Task<bool> SaveAsync<T>(T             config,
                                   DirectoryPath dirPath = null)
    {
      return SaveAsync(config, config.GetType(), dirPath);
    }

    /// <summary>Save the <paramref name="config" /> instance to file</summary>
    /// <param name="config">The config instance</param>
    /// <param name="configType">The <paramref name="config" />'s type</param>
    /// <param name="dirPath">Optional folder in which to save the configuration file</param>
    /// <returns>Success of operation</returns>
    public async Task<bool> SaveAsync(object        config,
                                      Type          configType,
                                      DirectoryPath dirPath = null)
    {
      dirPath ??= GetDefaultConfigDirectoryPath();

      try
      {
        using var stream = OpenConf(dirPath.FullPath, configType, FileAccess.Write);
        using var writer = new StreamWriter(stream);

        await writer.WriteAsync(JsonConvert.SerializeObject(config, Formatting.Indented)).ConfigureAwait(false);

        return true;
      }
      catch (Exception ex)
      {
        var filePath = GetConfigFilePath(dirPath, configType);
        LogTo.Warning(ex, "Failed to save config {FilePath}", filePath);

        throw new LOSException($"Failed to save config {filePath}", ex);
      }
    }

    /// <summary>Save the <paramref name="config" /> instance to file</summary>
    /// <param name="config">The config instance</param>
    /// <param name="dirPath">Optional folder in which to save the configuration file</param>
    /// <typeparam name="T">The configuration model</typeparam>
    /// <returns>Success of operation</returns>
    public bool Save<T>(T             config,
                        DirectoryPath dirPath = null)
    {
      return Save(config, config.GetType(), dirPath);
    }

    /// <summary>Save the <paramref name="config" /> instance to file</summary>
    /// <param name="config">The config instance</param>
    /// <param name="configType">The <paramref name="config" />'s type</param>
    /// <param name="dirPath">Optional folder in which to save the configuration file</param>
    /// <returns>Success of operation</returns>
    public bool Save(object        config,
                     Type          configType,
                     DirectoryPath dirPath = null)
    {
      dirPath ??= GetDefaultConfigDirectoryPath();

      try
      {
        using var stream = OpenConf(dirPath.FullPath, configType, FileAccess.Write);
        using var writer = new StreamWriter(stream);

        writer.Write(JsonConvert.SerializeObject(config, Formatting.Indented));

        return true;
      }
      catch (Exception ex)
      {
        var filePath = GetConfigFilePath(dirPath, configType);
        LogTo.Warning(ex, "Failed to save config {FilePath}", filePath);

        throw new LOSException($"Failed to save config {filePath}", ex);
      }
    }

    /// <summary>Opens a stream for the configuration file</summary>
    /// <param name="dirPath"></param>
    /// <param name="confType"></param>
    /// <param name="fileAccess"></param>
    /// <returns></returns>
    protected FileStream OpenConf(DirectoryPath dirPath,
                                  Type          confType,
                                  FileAccess    fileAccess)
    {
      if (!dirPath.Exists())
        return null;

      var filePath = GetConfigFilePath(dirPath, confType);

      return File.Open(
        filePath.FullPath,
        fileAccess == FileAccess.Read ? FileMode.OpenOrCreate : FileMode.Create,
        fileAccess,
        fileAccess == FileAccess.Read ? FileShare.Read : FileShare.None);
    }

    /// <summary>Builds and returns the final config file's path</summary>
    /// <param name="dirPath"></param>
    /// <param name="confType"></param>
    /// <returns></returns>
    protected virtual FilePath GetConfigFilePath(DirectoryPath dirPath,
                                                 Type          confType)
    {
      return dirPath.CombineFile(confType.Name + ".json");
    }

    /// <summary>Ensures the config folder exists</summary>
    protected void EnsureFolderExists()
    {
      var dirPath = GetDefaultConfigDirectoryPath();

      if (dirPath.Exists() == false)
        Directory.CreateDirectory(dirPath.FullPath);
    }

    #endregion




    #region Methods Abs

    /// <summary>
    ///   Returns the default directory in which to save the configuration files. This value is used when the dirPath parameter
    ///   of <see cref="LoadAsync{T}(DirectoryPath)" /> or <see cref="SaveAsync(object, Type, DirectoryPath)" /> is null.
    /// </summary>
    /// <returns></returns>
    protected abstract DirectoryPath GetDefaultConfigDirectoryPath();

    #endregion
  }
}
