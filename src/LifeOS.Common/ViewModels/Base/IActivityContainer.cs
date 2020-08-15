using GongSolutions.Wpf.DragDrop;
using LifeOS.Common.ViewModels.DayActivityVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace LifeOS.Common.ViewModels
{
  public interface IActivityContainer : IDropTarget
  {
    bool CanAcceptChildren { get; set; }
    ObservableCollection<DayActivityViewModel> Children { get; set; }

    void IDropTarget.DragOver(IDropInfo dropInfo)
    {
      IActivityContainer sourceItem = dropInfo.Data as IActivityContainer;
      IActivityContainer targetItem = dropInfo.TargetItem as IActivityContainer;

      if (sourceItem != null && targetItem != null && targetItem.CanAcceptChildren)
      {
        dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
        dropInfo.Effects = DragDropEffects.Move;
      }
    }

    void IDropTarget.Drop(IDropInfo dropInfo)
    {
      DayActivityViewModel sourceItem = dropInfo.Data as DayActivityViewModel;
      IActivityContainer targetItem = dropInfo.TargetItem as IActivityContainer;

      if (sourceItem != null && targetItem != null && targetItem.CanAcceptChildren)
      {
        targetItem.Children.Add(sourceItem);
      }
    }
  }
}
