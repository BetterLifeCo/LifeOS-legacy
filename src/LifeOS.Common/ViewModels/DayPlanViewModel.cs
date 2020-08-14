using GongSolutions.Wpf.DragDrop;
using LifeOS.Common.Models;
using LifeOS.Common.Sys.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LifeOS.Common.ViewModels
{

  public partial class DayPlanViewModel : IDropTarget
  {

    void IDropTarget.DragOver(IDropInfo dropInfo)
    {
      DayActivityViewModel sourceItem = dropInfo.Data as DayActivityViewModel;
      DayPlanViewModel targetItem = dropInfo.TargetItem as DayPlanViewModel;

      if (sourceItem != null && targetItem != null && targetItem.CanAcceptChildren)
      {
        dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
        dropInfo.Effects = DragDropEffects.Copy;
      }
    }

    void IDropTarget.Drop(IDropInfo dropInfo)
    {
      DayActivityViewModel sourceItem = dropInfo.Data as DayActivityViewModel;
      DayPlanViewModel targetItem = dropInfo.TargetItem as DayPlanViewModel;
      targetItem.Activities.Add(sourceItem);
    }

  }

  public partial class DayPlanViewModel : BaseViewModel
  {

    public DateTime Day { get; set; }
    public bool CanAcceptChildren { get; set; } = true;
    public ObservableCollection<DayActivityViewModel> Activities { get; set; }

    public DayPlanViewModel(DayPlan plan)
    {

      this.Day = plan.Day;
      this.Activities = new ObservableCollection<DayActivityViewModel>(
        plan.Activities.Select(x => new DayActivityViewModel(x)));

    }

  }
}
