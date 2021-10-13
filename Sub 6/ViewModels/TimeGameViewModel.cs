using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Sub_6.Commands;
using Sub_6.Messages;
using Sub_6.Models;
using Sub_6.Stores;
using Sub_6.Views;
using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Sub_6.ViewModels
{
    public class TimeGameViewModel : GameViewModel
    {
        HighScoreModel highscore = new HighScoreModel();
        public string Top1 { get; set; }
        public ICommand ResultViewCommand { get; }
        public string RemainingSeconds => string.Format("{0:hh\\:mm\\:ss}", TimeSpan.FromSeconds(_timerStore.RemainingSeconds));
        public ICommand StartCommand { get; }
        public ICommand QuitCommand { get; }
        //public int Duration { get; set; } = 120;

        #region
        #endregion

        //ChooseGameModeView chooseGameModeView;
        public TimeGameViewModel(NavigationStore navigationStore, TimerStore timerStore)
        {
            Top1 = highscore.GetTop1().ToString();
            _timerStore.RemainingSecondsChange += TimerStore_RemainingSecondsChanged;


            ResultViewModel rVM = new ResultViewModel(navigationStore, timerStore);
            ResultViewCommand = new ChangeViewCommand<ResultViewModel>(navigationStore, null, () => rVM);
            QuitCommand = new ChangeViewCommand<ChooseGenreViewModel>(navigationStore, null, () => new ChooseGenreViewModel(navigationStore, null));
        }


        #region Change colors on timer
        public SolidColorBrush BgColor { get; set; } = Brushes.Green;


        public void ChangeColor()
        {

            if (_timerStore.RemainingSeconds <= 30 && _timerStore.RemainingSeconds >= 10)
            {
                BgColor = Brushes.Yellow;

            }
            else if (_timerStore.RemainingSeconds <= 10)
            {
                BgColor = Brushes.Red;

            }
            else
            {
                BgColor = Brushes.LawnGreen;

            }
        }
        #endregion

        #region Music player
        public void Music()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\v\source\repos\SUP21_Grupp6\Sub 6\Media\123.wav");
            player.Play();
             
        }
        #endregion

        //Makes format from seconds to HH:MM:SS


        private void TimerStore_RemainingSecondsChanged()
        {
            OnPropertyChanged(nameof(RemainingSeconds));
            OnPropertyChanged(nameof(BgColor));
            ChangeColor();
            TimeIsUp();
            if (_timerStore._timer.Enabled != true)
            {
                mediaPlayer.Dispatcher.Invoke(() =>
                {
                    PlayBackgroundMusic();
                });
            }

        }
    
       


        public override void AddToScore()
        {
            score += 10;
            Player.Score = score;
            OnPropertyChanged("ScoreBind");
        }
       
        public string ScoreBind
        {
            get
            {
                return Player.Score.ToString();
            }
            set { OnPropertyChanged("ScoreBind"); }
        }


        public void TimeIsUp()
        {

            if (Cards.AllCards.Count == 0)
            {
                _timerStore.StopTime();
                var playerMessage = new PlayerMessage { PlayerScore = Player.Score += Convert.ToInt32(_timerStore.RemainingSeconds) };
                Messenger.Default.Send(playerMessage);
                mediaPlayer.Dispatcher.Invoke(() =>
                {
                    PlayBackgroundMusic();
                });
                DisplayCelebratoryGif();
                ResultViewCommand.Execute(null);
            }
            if (Convert.ToInt32(_timerStore.RemainingSeconds) < 1)
            {

                _timerStore.StopTime();
                var playerMessage = new PlayerMessage { PlayerScore = Player.Score };
                Messenger.Default.Send(playerMessage);
                DisplayCelebratoryGif();
                ResultViewCommand.Execute(null);
            }

            else
            {
                // Do nothing
            }

        }
        
    }
}
