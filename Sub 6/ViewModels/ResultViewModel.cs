using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Npgsql;
using Sub_6.Commands;
using Sub_6.Messages;
using Sub_6.Models;
using Sub_6.Stores;
using Sub_6.Views;

namespace Sub_6.ViewModels
{
    public class ResultViewModel : GameViewModel
    {
        #region Objects
        private readonly HighScoreModel highScore = new HighScoreModel();

        #endregion
        #region Properties
        public ObservableCollection<PlayerViewModel> HighScorePlayers { get; set; }
        public string Name { get; set; }
        public bool TextBoxEnabled { get; set; } = true;
        public bool BtnSaveEnabled { get; set; } = true;
        public string ThisPlayerScoreRank { get; set;} // Binds score,name and rank to label.
        public string ShowPlayerScoreRank { get; set;} = "Hidden"; // Hides or shows the score and rank for the current player.
        #endregion


        #region UpdateCommand
        private RelayCommand _updateScoreCommand;
        public ICommand PlayAgainCommand { get; }

    public RelayCommand UpdatedScoreCommand
        {
            get
            {
                if (_updateScoreCommand == null)
                {
                    _updateScoreCommand = new RelayCommand(UpdateScoreMethod);
                }
                return _updateScoreCommand;
            }
            set { }
        }

        /// <summary>
        /// When button saved is clicked, textbox is clear and hidden, button is hidden, Highscore list is updated.
        /// </summary>
        private void UpdateScoreMethod()
        {
            if (Name != null) // If the player has put in a name, execute method, otherwise do nothing.
            {
                AddPlayerScoreToHighScore(Name); //Adds the player to Highscorelist. 
                HighScorePlayers = highScore.GetTop5();
                Name = null; // Clears the textbox.
                OnPropertyChanged(nameof(HighScorePlayers));
                OnPropertyChanged(nameof(Name));
                TextBoxEnabled = false; // Disables textbox where you put in your name.
                BtnSaveEnabled = false; // Disables the button to submit your stats.
                OnPropertyChanged(nameof(TextBoxEnabled));
                OnPropertyChanged(nameof(BtnSaveEnabled));

            }

        }
      
       
        #endregion
        #region ctor
        public ResultViewModel(NavigationStore navigationStore, TimerStore timerStore) 
        {
            Messenger.Default.Register<PlayerMessage>(this, (playerMessage) => /// receives score from game 

            {

                Player.Score = playerMessage.PlayerScore;

            });

            PlayAgainCommand = new ChangeViewCommand<ChooseGenreViewModel>(navigationStore, timerStore, () => new ChooseGenreViewModel(navigationStore, timerStore));
            

            HighScorePlayers = highScore.GetTop5(); ///Retrives the list in listbox
            DisplayCelebratoryGif(); //Displays gifs when you beat the game.

        }
        public string Score
        {
            get
            {
                int score = Player.Score;
                return score.ToString();
            }
            set { OnPropertyChanged("Score"); }
        }
        #endregion
        #region methods
      

        /// <summary>
        /// Method to add current player score to scorelist
        /// </summary>
        /// <param name="name"></param>
        public void AddPlayerScoreToHighScore(string name) //körs när player valt att spara sitt score
        {
            
            Player.Name = name;
            Player.ID = highScore.AddHigh(Player);
            if (Player.IsTop5 == true)
            {
                HighScorePlayers = highScore.GetTop5();
                
            }
            else
            {
                ThisPlayerScoreRank = highScore.GetPlayerRank(Player.ID);
                ShowPlayerScoreRank = "Visible";
                OnPropertyChanged(nameof(ThisPlayerScoreRank));
                OnPropertyChanged(nameof(ShowPlayerScoreRank));
            }
            
           

        }
       

     
        #endregion

    }
}
