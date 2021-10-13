
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Sub_6.Commands;
using Sub_6.Messages;
using Sub_6.Models;
using Sub_6.Stores;
using Sub_6.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sub_6.ViewModels
{


    class ChooseGameModeViewModel : GameViewModel

    {
        #region Commands
        public ICommand ChooseCasualGameCommand { get; }
        public ICommand ChooseTimeGameCommand { get; }
        public ICommand GoBackCommand { get; }
        #endregion

        #region Properties
        public ImageSource MatchCardByGenre
        {
            get
            {
                return MatchCard();
            }
            set { OnPropertyChanged("MatchCardByGenre"); }
        }
        public ImageSource NoMatchCardByGenre
        {
            get
            {
                return NoMatchCard();
            }
            set { OnPropertyChanged("NoMatchCardByGenre"); }
        }
        #endregion

        #region cotr
        public ChooseGameModeViewModel(NavigationStore navigationStore, TimerStore timerStore)
        {
            
            CasualGameViewModel cgVM = new CasualGameViewModel(navigationStore);
            TimeGameViewModel tgVM = new TimeGameViewModel(navigationStore, timerStore);
            GameViewModel gVM = new GameViewModel();

            GoBackCommand = new ChangeViewCommand<ChooseGenreViewModel>(navigationStore, timerStore, () => new ChooseGenreViewModel(navigationStore, timerStore));
            ChooseCasualGameCommand = new ChangeViewCommand<CasualGameViewModel>(navigationStore, null, () => cgVM);
            ChooseTimeGameCommand = new ChangeViewCommand<TimeGameViewModel>(navigationStore, timerStore, () => tgVM);
           
        }
        #endregion

        #region Methods
        //both methods display the "tutorial" images with the specific sound cards for the genre chosen by the player.
        private ImageSource MatchCard()
        {
            string name = null;

            if (genreID == 2)
            {
                name = "https://i.ibb.co/9tMBtDD/scifi-match.png";
            }
            if (genreID == 1)
            {
                name = "https://i.ibb.co/YZnGSR0/music-match.png";
            }
            if (genreID == 0)
            {
                name = "https://i.ibb.co/pZyybTs/animal-match.png";
            }

            
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(name);
            b.EndInit();

            return b;
        }
        private ImageSource NoMatchCard()
        {
            string name = null;

            if (genreID == 2)
            {
                name = "https://i.ibb.co/xChCq4Q/scifi-nomatch.png";
            }
            if (genreID == 1)
            {
                name = "https://i.ibb.co/gMLZ0kr/music-nomatch.png";
            }
            if (genreID == 0)
            {
                name = "https://i.ibb.co/3c1xg8s/animal-nomatch.png";
            }

            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(name, UriKind.RelativeOrAbsolute);
            b.EndInit();

            return b;
        }
        #endregion





    }
}

