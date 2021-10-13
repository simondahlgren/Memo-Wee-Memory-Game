using GalaSoft.MvvmLight.Messaging;
using Sub_6.Commands;
using Sub_6.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sub_6.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        

        public BaseViewModel()
        {
            
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

     
    }
}
