using Sub_6.Commands;
using Sub_6.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Sub_6.ViewModels
{
    class SettingsViewModel : GameViewModel
    {
        public ICommand GoBackCommand { get; }
        public SettingsViewModel(NavigationStore navigationStore)
        {
            GoBackCommand = new ChangeViewCommand<ChooseGenreViewModel>(navigationStore, null, () => new ChooseGenreViewModel(navigationStore, null));
        }
    }
}
