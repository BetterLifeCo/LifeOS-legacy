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
// Created On:   2018/05/31 13:45
// Modified On:  2019/01/21 14:41
// Modified By:  Alexis

#endregion




namespace LifeOS.Common.Extensions
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// Extension methods for <see cref="Object"/>
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
  public static class ObjectEx
  {
    #region Methods

    /// <summary>
    /// Executes an action on <paramref name="obj"/> and then returns it. Allows chaining actions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="withAction"></param>
    /// <returns></returns>
    public static T With<T>(this T    obj,
                            Action<T> withAction)
    {
      withAction(obj);

      return obj;
    }

    /// <summary>
    /// Checks whether <paramref name="obj"/> is contained within the collection <paramref name="col"/>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="obj"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public static bool ContainedIn<T1, T2>(this T1         obj,
                                           IEnumerable<T2> col)
      where T2 : T1
    {
      return col.Any(e => e?.Equals(obj) ?? false);
    }

    #endregion
  }
}
