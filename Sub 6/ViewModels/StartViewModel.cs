using Sub_6.Commands;
using Sub_6.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Sub_6.ViewModels
{
    public class StartViewModel : GameViewModel
    {
        //the ChangeViewCommand can be used in different forms depending on what we want to link to. The rename is local with a newing call to the Command and specifications for local parameters (see constructor)
        public ICommand NavigateToGenreCommand { get; }

        public StartViewModel(NavigationStore navigationStore, TimerStore timerStore)
        {
            NavigateToGenreCommand = new ChangeViewCommand<ChooseGenreViewModel>(navigationStore, null, () => new ChooseGenreViewModel(navigationStore, null));
        }
        
    }
}
