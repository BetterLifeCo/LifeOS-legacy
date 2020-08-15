using LifeOS.Common.Models;
using LifeOS.Common.Sys.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Input;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using LifeOS.Common.Extensions;

namespace LifeOS.Common.ViewModels.DayActivityVM
{

  public partial class DayActivityViewModel : BaseViewModel, IActivityContainer
  {

    public ActivityTimeSlot TimeSlot { get; set; }
    public string ActivityTitle { get; set; }
    public bool CanAcceptChildren { get; set; } = true;
    public ObservableCollection<DayActivityViewModel> Children { get; set; }

    public DayActivityViewModel(DayActivity activity)
    {
      ThrowIfNull(activity);

      this.TimeSlot = activity.TimeSlot;
      this.ActivityTitle = activity.ActivityTitle;
      this.Children = new ObservableCollection<DayActivityViewModel>(activity.SubActivities.Select(x => new DayActivityViewModel(x)));
    }

    private void ThrowIfNull(DayActivity activity)
    {
      ExceptionEx.ThrowIfArgumentNull(activity, "Failed to create DayActivityViewModel because DayActivity was null");
      ExceptionEx.ThrowIfArgumentNull(activity.SubActivities, "Failed to create DayActivityViewModel because SubActivities were null");
    }

    public void AddSubActivity(DayActivity activity)
    {

      this.Children.Add(new DayActivityViewModel(activity));

    }

    public void DeleteSubActivity(DayActivityViewModel activity)
    {

      this.Children.Remove(activity);

    }

  }
}
