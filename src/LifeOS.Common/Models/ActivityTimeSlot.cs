// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityTimeSlot.cs" company="">
//   
// </copyright>
// <summary>
//   The activity time slot.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
// Created On:   2020/08/05 20:22
// Modified On:  2020/08/05 20:56
// Modified By:  Alexis

#endregion




namespace LifeOS.Common.Models
{
  using NodaTime;

  /// <summary>
  /// The activity time slot.
  /// </summary>
  public class ActivityTimeSlot
  {
    #region Properties & Fields - Non-Public

    /// <summary>
    /// Gets or sets the duration.
    /// </summary>
    public Duration   Duration { get; private set; }

    /// <summary>
    /// Gets or sets the time.
    /// </summary>
    public OffsetTime Time     { get; private set; }

    public ActivityTimeSlot() { }

    public ActivityTimeSlot(Duration duration, OffsetTime time)
    {
      this.Duration = duration;
      this.Time = time;
    }

    #endregion
  }
}
