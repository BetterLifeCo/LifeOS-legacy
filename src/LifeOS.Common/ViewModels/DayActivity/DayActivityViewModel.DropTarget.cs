using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace LifeOS.Common.ViewModels.DayActivityVM
{
  public partial class DayActivityViewModel : IDropTarget
  {

    void IDropTarget.Drop(IDropInfo dropInfo)
    {

      DayActivityViewModel activity = dropInfo.Data as DayActivityViewModel;
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
}
