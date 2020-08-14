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

namespace LifeOS.Common.ViewModels
{

  // Implement Gong DragDrop methods
  public partial class DayActivityViewModel : IDropTarget
  {

    void IDropTarget.Drop(IDropInfo dropInfo)
    {

      var activity = dropInfo.Data as DayActivityViewModel;
      DayActivityViewModel targetItem = dropInfo.TargetItem as DayActivityViewModel;
      targetItem.SubActivities.Add(activity);

    }

    void IDropTarget.DragOver(IDropInfo dropInfo)
    {

      DayActivityViewModel sourceItem = dropInfo.Data as DayActivityViewModel;
      DayActivityViewModel targetItem = dropInfo.TargetItem as DayActivityViewModel;

      if (sourceItem != null && targetItem != null)
      {
        dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
        dropInfo.Effects = DragDropEffects.Copy;
      }

    }
  }

  public partial class DayActivityViewModel : BaseViewModel
  {

    public ActivityTimeSlot TimeSlot { get; set; }
    public string ActivityTitle { get; set; }
    public ObservableCollection<DayActivityViewModel> SubActivities { get; set; }

    public DayActivityViewModel(DayActivity activity)
    {

      this.TimeSlot = activity.TimeSlot;
      this.ActivityTitle = activity.ActivityTitle;
      this.SubActivities = activity.SubActivities == null
        ? new ObservableCollection<DayActivityViewModel>()
        : new ObservableCollection<DayActivityViewModel>(activity.SubActivities.Select(x => new DayActivityViewModel(x)));

    }


    public void AddSubActivity(DayActivity activity)
    {

      this.SubActivities.Add(new DayActivityViewModel(activity));

    }

  }
}
