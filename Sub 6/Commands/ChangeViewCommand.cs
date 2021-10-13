
using Sub_6.Stores;
using Sub_6.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Sub_6.Commands
{
    public class ChangeViewCommand<TViewModel> : CommandBase
        where TViewModel : GameViewModel //constraint needed becase currentviewmodel must be a Gameviewmodel (due to our inheritances) and Tviewmodel wasn't specified for that
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _loadViewModel;

        //Function created to allow parameter in the Type of ViewModel to be used as command parameter, allowing easy Binding in buttons etc. The Function triggers (loads) the Type.
        public ChangeViewCommand(NavigationStore navigationStore, TimerStore timerStore, Func<TViewModel> loadViewModel) 
        {
            _navigationStore = navigationStore;
            _loadViewModel = loadViewModel;
        }
       //when command executes, the new current VM (determined by navigationstore) is loaded 
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _loadViewModel();
        }
    }
}
