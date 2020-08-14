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

  public partial class DayActivityViewModel : BaseViewModel
  {

    public ActivityTimeSlot TimeSlot { get; set; }
    public string ActivityTitle { get; set; }
    public ObservableCollection<DayActivityViewModel> SubActivities { get; set; }

    public DayActivityViewModel(DayActivity activity)
    {
      ThrowIfNull(activity);

      this.TimeSlot = activity.TimeSlot;
      this.ActivityTitle = activity.ActivityTitle;
      this.SubActivities = new ObservableCollection<DayActivityViewModel>(activity.SubActivities.Select(x => new DayActivityViewModel(x)));
    }

    private void ThrowIfNull(DayActivity activity)
    {
      ExceptionEx.ThrowIfArgumentNull(activity, "Failed to create DayActivityViewModel because DayActivity was null");
      ExceptionEx.ThrowIfArgumentNull(activity.SubActivities, "Failed to create DayActivityViewModel because SubActivities were null");
    }

    public void AddSubActivity(DayActivity activity)
    {

      this.SubActivities.Add(new DayActivityViewModel(activity));

    }

    public void DeleteSubActivity(DayActivityViewModel activity)
    {

      this.SubActivities.Remove(activity);

    }

  }
}
