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
// Created On:   2020/08/05 20:00
// Modified On:  2020/08/05 20:56
// Modified By:  Alexis

#endregion




namespace LifeOS.WPF.Views.Windows
{
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Linq;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Media;
  using LifeOS.Common.Models;
  using LifeOS.Common.ViewModels;
  using LifeOS.Common.ViewModels.DayActivityVM;
  using LifeOS.Common.ViewModels.DayPlanVM;
  using MahApps.Metro.Controls;
  using NodaTime;

  public class MainViewModel : BaseViewModel
  {
    
    public DayPlanViewModel DayPlan { get; set; }
    
    public MainViewModel()
    {

      DayPlan = new DayPlanViewModel(new DayPlan());

    }

  }

  /// <summary>Interaction logic for MainWindow.xaml</summary>
  public partial class MainWindow : MetroWindow
  {
    #region Constructors

    public DayPlanViewModel Plan { get; set; }
    public ObservableCollection<DayActivityViewModel> ActivityTemplates { get; set; }
    
    public MainWindow()
    {
      
      InitializeComponent();

      var timeslot = new ActivityTimeSlot();
      timeslot.Duration = NodaTime.Duration.FromHours(1);
      timeslot.Time = new OffsetTime(
        new LocalTime(15, 20, 48),
        Offset.FromHours(0));

      var subtimeslot = new ActivityTimeSlot();
      subtimeslot.Duration = NodaTime.Duration.FromHours(0.5);
      subtimeslot.Time = new OffsetTime(
        new LocalTime(15, 20, 48),
        Offset.FromHours(0));

      var subactivity = new DayActivity(subtimeslot, "Incremental Reading", Enumerable.Empty<DayActivity>());
      var activity1 = new DayActivity(timeslot, "SuperMemo", new List<DayActivity> { subactivity });


      var plan = new DayPlan();
      plan.Activities = new ObservableCollection<DayActivity>();
      plan.Activities.Add(activity1);

      Plan = new DayPlanViewModel(plan);

      ActivityTemplates = new ObservableCollection<DayActivityViewModel>
      {

        new DayActivityViewModel(
          new DayActivity(
            new ActivityTimeSlot(NodaTime.Duration.FromHours(1), new OffsetTime(new LocalTime(15, 20, 48), Offset.FromHours(0))),
            "Hello World",
            Enumerable.Empty<DayActivity>()))

      };

      this.DataContext = this;

    }

    private void Expander_Expanded(object sender, RoutedEventArgs e)
    {
      for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
        if (vis is DataGridRow)
        {
          var row = (DataGridRow)vis;
          row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
          break;
        }
    }

    private void Expander_Collapsed(object sender, RoutedEventArgs e)
    {
      for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
        if (vis is DataGridRow)
        {
          var row = (DataGridRow)vis;
          row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
          break;
        }
    }

    #endregion
  }
}
