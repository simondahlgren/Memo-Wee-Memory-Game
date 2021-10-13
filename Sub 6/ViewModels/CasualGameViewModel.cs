using GalaSoft.MvvmLight.Messaging;
using Sub_6.Commands;
using Sub_6.Messages;
using Sub_6.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sub_6.ViewModels
{
    public class CasualGameViewModel : GameViewModel
    {
        public ICommand GameOverViewCommand { get; }
        public ICommand QuitCommand { get; }

        public CasualGameViewModel(NavigationStore navigationStore)
        {
            //new GameViewModel is instantiated in constructor to be loaded and ready to receieve a playermessage.
            GameOverViewModel goVM = new GameOverViewModel(navigationStore, null);
            GameOverViewCommand = new ChangeViewCommand<GameOverViewModel>(navigationStore, null, () => goVM);
            QuitCommand = new ChangeViewCommand<ChooseGenreViewModel>(navigationStore, null, () => new ChooseGenreViewModel(navigationStore, null));
        }

 


        //allows the final 2 cards to be viewed by the player before the game ends.
        public async override void FinalPair()
        {
            if (Cards.AllCards.Count == 2 && flippedCards.Count == 2)
            {
                await Task.Delay(2500);
                Cards.AllCards.Clear();
                mediaPlayer.Stop();
                GameIsOver();
            }
        }


        public void GameIsOver()
        {
            //when cards on board runt out, navigation to next view is triggered.
            if (Cards.AllCards.Count == 0)
            {

                GameOverViewCommand.Execute(null);

            }
           

        }
    }
}

