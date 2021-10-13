using GalaSoft.MvvmLight.Command;
using Sub_6.Commands;
using Sub_6.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Sub_6.ViewModels
{
    class GameOverViewModel : GameViewModel
    {

        private RelayCommand _playAgainNavigate;
        public ICommand PlayAgainCommand { get; }

        public RelayCommand PlayAgainNavigate
        {
            get
            {
                if (_playAgainNavigate == null)
                {
                    _playAgainNavigate = new RelayCommand(PlayAgainNavigation);
                }
                return _playAgainNavigate;
            }
            set { }
        }
        public void PlayAgainNavigation()
        {

            PlayAgainCommand.Execute(null);
        }
        public GameOverViewModel(NavigationStore navigationStore, TimerStore timerStore)
        {
            PlayAgainCommand = new ChangeViewCommand<ChooseGenreViewModel>(navigationStore, timerStore, () => new ChooseGenreViewModel(navigationStore, timerStore));
        }
    }
}
