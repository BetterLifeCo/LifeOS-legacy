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

  public partial class DayPlanViewModel : BaseViewModel
  {

    public DateTime Day { get; set; }
    public bool CanAcceptChildren { get; set; } = true;
    public ObservableCollection<DayActivityViewModel> Activities { get; set; }

    public DayPlanViewModel(DayPlan plan)
    {
      ThrowIfNull(plan);

      this.Day = plan.Day;
      this.Activities = new ObservableCollection<DayActivityViewModel>(plan.Activities.Select(x => new DayActivityViewModel(x)));
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
      this.Activities.Remove(activity);
    }

    public void DeleteActivity(DayActivityViewModel activity)
    {
      this.Activities.Remove(activity);
    }

  }
}
