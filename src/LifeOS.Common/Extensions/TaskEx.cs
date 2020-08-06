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




namespace LifeOS.Common.Extensions
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Threading.Tasks;

  public static class TaskEx
  {
    #region Methods
    
    [SuppressMessage("AsyncUsage", "AsyncFixer03:Avoid fire & forget async void methods")]
    [SuppressMessage("Usage", "VSTHRD100:Avoid async void methods")]
    [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods")]
    [SuppressMessage("Usage", "VSTHRD003:Avoid awaiting foreign Tasks")]
    public static async void RunAsync(this Task         task,
                                      Action<Exception> handler = null)
    {
      if (task == null)
        throw new ArgumentNullException(nameof(task));

      try
      {
        await task.ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        handler?.Invoke(ex);
      }
    }
    
    [SuppressMessage("AsyncUsage", "AsyncFixer03:Avoid fire & forget async void methods")]
    [SuppressMessage("Usage", "VSTHRD100:Avoid async void methods")]
    [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods")]
    [SuppressMessage("Usage", "VSTHRD003:Avoid awaiting foreign Tasks")]
    public static async void RunAsync(this Task             task,
                                      Func<Exception, Task> handler)
    {
      if (task == null)
        throw new ArgumentNullException(nameof(task));

      try
      {
        await task.ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        await (handler?.Invoke(ex)).ConfigureAwait(false);
      }
    }

    /// <summary>Convenience task for async methods that aren't implemented</summary>
    /// <returns></returns>
    public static Task ThrowNotImplementedExceptionAsync()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
