using GongSolutions.Wpf.DragDrop;
using LifeOS.Common.Extensions;
using LifeOS.Common.Models;
using LifeOS.Common.Sys.ComponentModel;
using LifeOS.Common.ViewModels.DayActivityVM;
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

namespace LifeOS.Common.ViewModels.DayPlanVM
{

  public partial class DayPlanViewModel : BaseViewModel, IActivityContainer
  {

    public DateTime Day { get; set; }
    public bool CanAcceptChildren { get; set; } = true;
    public ObservableCollection<DayActivityViewModel> Children { get; set; }

    public DayPlanViewModel(DayPlan plan)
    {
      ThrowIfNull(plan);

      this.Day = plan.Day;
      this.Children = new ObservableCollection<DayActivityViewModel>(plan.Activities.Select(x => new DayActivityViewModel(x)));
    }

    private void ThrowIfNull(DayPlan plan)
    {
      ExceptionEx.ThrowIfArgumentNull(plan, "Failed to create DayPlanViewModel because DayPlan was null");
      ExceptionEx.ThrowIfArgumentNull(plan, "Failed to create DayPlanViewModel because DayPlan's Activities were null");
    }

    public void SaveDayPlan()
    {
      throw new NotImplementedException();
    }

    public void DeleteDayPlan()
    {
      throw new NotImplementedException();
    }

    public void AddActivity(DayActivityViewModel activity)
    {
      this.Children.Remove(activity);
    }

    public void DeleteActivity(DayActivityViewModel activity)
    {
      this.Children.Remove(activity);
    }

  }
}
