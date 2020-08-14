using GongSolutions.Wpf.DragDrop;
using LifeOS.Common.ViewModels.DayActivityVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace LifeOS.Common.ViewModels.DayPlanVM
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
}
