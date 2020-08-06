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




// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace LifeOS.Common.Extensions
{
  using System;
  using System.Threading.Tasks;
  using Nito.AsyncEx;

  /// <summary>Extension methods for Nito.AsyncEx</summary>
  public static class NitoAsyncEx
  {
    #region Methods

    /// <summary>
    ///   Asynchronously waits for the <paramref name="waitHandle" /> to signal. Cancels after <paramref name="timeoutMs" />
    ///   milliseconds
    /// </summary>
    /// <param name="waitHandle">The event to wait on</param>
    /// <param name="timeoutMs">The maximum amount of time to wait for, in milliseconds</param>
    /// <returns><see langword="true" /> if the event was signaled, false if it timed out.</returns>
    public static async Task<bool> WaitAsync(this AsyncManualResetEvent waitHandle,
                                             int                        timeoutMs)
    {
      if (waitHandle == null)
        throw new ArgumentNullException(nameof(waitHandle));

      Task waitTask      = waitHandle.WaitAsync();
      Task completedTask = await Task.WhenAny(Task.Delay(timeoutMs), waitHandle.WaitAsync()).ConfigureAwait(false);

      return completedTask == waitTask;
    }

    /// <summary>
    ///   Asynchronously waits for the <paramref name="waitHandle" /> to signal. Cancels after <paramref name="timeoutMs" />
    ///   milliseconds
    /// </summary>
    /// <param name="waitHandle">The event to wait on</param>
    /// <param name="timeoutMs">The maximum amount of time to wait for, in milliseconds</param>
    /// <returns><see langword="true" /> if the event was signaled, false if it timed out.</returns>
    public static async Task<bool> WaitAsync(this AsyncAutoResetEvent waitHandle,
                                             int                      timeoutMs)
    {
      if (waitHandle == null)
        throw new ArgumentNullException(nameof(waitHandle));

      Task waitTask      = waitHandle.WaitAsync();
      Task completedTask = await Task.WhenAny(Task.Delay(timeoutMs), waitHandle.WaitAsync()).ConfigureAwait(false);

      return completedTask == waitTask;
    }

    #endregion
  }
}
