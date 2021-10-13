
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using Sub_6.Commands;
using Sub_6.Messages;
using Sub_6.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Sub_6.Models;

namespace Sub_6.ViewModels
{
    public class ChooseGenreViewModel : GameViewModel
    {
        #region commands
        public ICommand NavigateToGameModeCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }
        private RelayCommand<int> _pickedGenreCommand;

        public RelayCommand<int> PickedGenreCommand
        {
            get
            {
                if (_pickedGenreCommand == null)
                {
                    _pickedGenreCommand = new RelayCommand<int>(SendPickedGenre);
                }
                return _pickedGenreCommand;
            }
            set { }
        }
        #endregion

        #region cotr
        public ChooseGenreViewModel(NavigationStore navigationStore, TimerStore timerStore)
        {
            //a new ChooseGameModeViewModel is instantiated in constructor to be loaded and ready to recieve genremessage when sent. (It must exist to be able to recieve)
            ChooseGameModeViewModel cgmVM = new ChooseGameModeViewModel(navigationStore, timerStore);

            NavigateToGameModeCommand = new ChangeViewCommand<ChooseGameModeViewModel>(navigationStore, timerStore, () => cgmVM);
            NavigateToSettingsCommand = new ChangeViewCommand<SettingsViewModel>(navigationStore, timerStore, () => new SettingsViewModel(navigationStore));
         
        }
        #endregion
        //Sends the picked genre on to ChooseGameModeVM as property of GenreMessage.
        private void SendPickedGenre(int genre)
        {
            var genreMessage = new GenreMessage { GenrePick = genre };
      
            Messenger.Default.Send(genreMessage);
        }


    }
}
