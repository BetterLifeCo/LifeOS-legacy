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
// Created On:   2019/01/21 14:42
// Modified On:  2019/01/21 14:44
// Modified By:  Alexis

#endregion




namespace LifeOS.Common.Extensions
{
  using System.IO;
  using System.Threading.Tasks;
  using global::Extensions.System.IO;
  using Newtonsoft.Json;

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
  public static class JsonEx
  {
    #region Methods

    public static string Serialize(this object obj,
                                   Formatting  format = Formatting.None)
    {
      return JsonConvert.SerializeObject(obj, format);
    }

    public static T Deserialize<T>(this string json)
    {
      return JsonConvert.DeserializeObject<T>(json);
    }

    public static Task SerializeToFileAsync(this object obj,
                                             FilePath    filePath,
                                             Formatting  format = Formatting.None)
    {
      using var fs = File.Open(filePath.FullPath, FileMode.Create, FileAccess.Write);
      using var writer = new StreamWriter(fs);

      return writer.WriteAsync(obj.Serialize(format));
    }

    public static async Task<T> DeserializeFromFileAsync<T>(this FilePath filePath)
    {
      using var fs = File.Open(filePath.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
      using var reader = new StreamReader(fs);

      return Deserialize<T>(await reader.ReadToEndAsync().ConfigureAwait(false));
    }

    #endregion
  }
}
