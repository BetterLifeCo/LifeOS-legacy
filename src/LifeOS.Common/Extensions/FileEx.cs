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
// Modified On:  2020/04/07 07:11
// Modified By:  Alexis

#endregion




namespace LifeOS.Common.Extensions
{
  using System.Globalization;
  using System.IO;
  using global::Extensions.System.IO;
  using Sys.Security.Cryptography;

  public static class FileEx
  {
    #region Methods

    /// <summary>Computes the CRC32 for the given file</summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetCrc32(FilePath filePath)
    {
      using (var crc32 = new Crc32())
      {
        var hash = string.Empty;

        using (var fs = File.Open(filePath.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
          foreach (byte b in crc32.ComputeHash(fs))
            hash += b.ToString("x2", CultureInfo.InvariantCulture).ToLowerInvariant();

        return hash;
      }
    }

    #endregion
  }
}
