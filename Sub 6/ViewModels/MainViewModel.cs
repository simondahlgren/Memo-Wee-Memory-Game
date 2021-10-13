
using Sub_6.Commands;
using Sub_6.Messages;
using Sub_6.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Sub_6.ViewModels
{
    public class MainViewModel : GameViewModel
    {
        private readonly NavigationStore _navigationStore;
        //the gVM that is the current viewmodel = the CurrentViewModel determined  by the navigationstore
        public GameViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
           
            //subscribe to event to update current viewmodel
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;  
        }

        private void OnCurrentViewModelChanged()
        {
            //connects through INotifyPropertyChangedin BaseViewModel to update CurrentViewModel
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
