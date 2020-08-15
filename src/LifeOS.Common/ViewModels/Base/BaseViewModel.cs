using LifeOS.Common.Sys.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace LifeOS.Common.ViewModels
{

  public abstract class BaseViewModel : INotifyPropertyChangedEx
  {

    [JsonIgnore]
    public bool IsChanged { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

  }

}
